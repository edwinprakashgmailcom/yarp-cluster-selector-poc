﻿namespace Proxy;

public static class ClusterSelectorMiddlewareExtensions
{
    /// <summary>
    /// Selects the appropriate cluster.
    /// </summary>
    public static IReverseProxyApplicationBuilder UseClusterSelector(this IReverseProxyApplicationBuilder builder)
    {
        builder.UseMiddleware<ClusterSelectorMiddleware>();
        return builder;
    }
}