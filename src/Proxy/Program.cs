using Microsoft.FeatureManagement;

namespace Proxy;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        string connectionString = builder.Configuration.GetConnectionString("AppConfig");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        builder.Configuration.AddAzureAppConfiguration(options =>
        {
            options.Connect(connectionString)
                .ConfigureRefresh(refreshOptions =>
                    refreshOptions.Register("Settings:Sentinel", refreshAll: true));
            options.UseFeatureFlags();
        });

        builder.Services.AddAzureAppConfiguration();
        builder.Services.AddFeatureManagement();

        var proxyConfig = builder.Configuration.GetSection("ReverseProxy");
        builder.Services.AddReverseProxy()
                .LoadFromConfig(proxyConfig);

        var app = builder.Build();

        app.UseAzureAppConfiguration(); 
        
        app.MapReverseProxy(proxyPipeline =>
        {
            proxyPipeline.UseClusterSelector();
            proxyPipeline.UseSessionAffinity();
            proxyPipeline.UseLoadBalancing();
            proxyPipeline.UsePassiveHealthChecks();
        });

        app.Run();
    }
}