namespace Proxy;

internal sealed partial class ClusterSelectorMiddleware
{
    private static class Log
    {
        private static readonly Action<ILogger, string, Exception?> _noFeatureFoundOrFeatureDisabled = LoggerMessage.Define<string>(
            LogLevel.Information,
            EventIds.NoFeatureFoundOrFeatureDisabled,
            "Feature '{featureName}' is diabled or does not exist.");

        private static readonly Action<ILogger, string, Exception?> _featureEnabled = LoggerMessage.Define<string>(
            LogLevel.Information,
            EventIds.FeatureEnabled,
            "Feature '{featureName}' is enabled.");

        private static readonly Action<ILogger, string, Exception?> _noClusterFound = LoggerMessage.Define<string>(
            LogLevel.Error,
            EventIds.NoClusterFound,
            "Cluster '{clusterName}' not found.");

        private static readonly Action<ILogger, string, Exception?> _clusterReassigned = LoggerMessage.Define<string>(
            LogLevel.Information,
            EventIds.ClusterReassigned,
            "Cluster reassigned from default to '{clusterName}'.");

        public static void NoFeatureFoundOrFeatureDisabled(ILogger logger, string featureName)
        {
            _noFeatureFoundOrFeatureDisabled(logger, featureName, null);
        }

        public static void FeatureEnabled(ILogger logger, string featureName)
        {
            _featureEnabled(logger, featureName, null);
        }

        public static void NoClusterFound(ILogger logger, string clusterName)
        {
            _noClusterFound(logger, clusterName, null);
        }

        public static void ClusterReassigned(ILogger logger, string clusterName)
        {
            _clusterReassigned(logger, clusterName, null);
        }
    }
}
