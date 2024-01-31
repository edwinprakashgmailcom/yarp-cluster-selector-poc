using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Yarp.ReverseProxy;

namespace Proxy;

internal sealed partial class ClusterSelectorMiddleware
{
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public ClusterSelectorMiddleware(
        RequestDelegate next,
        ILogger<ClusterSelectorMiddleware> logger
        )
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var resource = GetResource(context);

        var featureManager = context.RequestServices.GetRequiredService<IFeatureManager>();
        var newRouteEnabled = await featureManager.IsEnabledAsync(resource);

        if (newRouteEnabled)
        {
            Log.FeatureEnabled(_logger, resource);

            const string newClusterName = "newcluster";

            var lookup = context.RequestServices.GetRequiredService<IProxyStateLookup>();
            if (lookup.TryGetCluster(newClusterName, out var cluster))
            {
                context.ReassignProxyRequest(cluster);
                Log.ClusterReassigned(_logger, newClusterName);
            }
            else
            {
                Log.NoClusterFound(_logger, newClusterName);
            }
        }
        else
        {
            Log.NoFeatureFoundOrFeatureDisabled(_logger, resource);
        }

        await _next(context);
    }

    string GetResource(HttpContext context)
    {
        return context.Request.Path.HasValue
            ? string.IsNullOrEmpty(context.Request.Path.Value)
                ? string.Empty
                : context.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty
            : string.Empty;
    }
}
