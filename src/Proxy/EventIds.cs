namespace Proxy;

internal static class EventIds
{
    public static readonly EventId NoClusterFound = new EventId(4, "NoClusterFound");
    public static readonly EventId NoFeatureFoundOrFeatureDisabled = new EventId(7, "NoFeatureFoundOrFeatureDisabled");
    public static readonly EventId FeatureEnabled = new EventId(8, "FeatureEnabled");
    public static readonly EventId ClusterReassigned = new EventId(9, "ClusterReassigned");
}
