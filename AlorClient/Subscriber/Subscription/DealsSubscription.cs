using System.Text.Json.Serialization;

namespace AlorClient
{
    public sealed class DealsSubscription : SecuritySubscription
    {
        [JsonPropertyName("includeVirtualTrades\"")]
        public bool IncludeVirtualTrades { get; }

        [JsonPropertyName("depth")]
        public int Depth { get; }

        public DealsSubscription(Security security, bool includeVirtualTrades, int depth) : base(security, "AllTradesGetAndSubscribe")
        {
            IncludeVirtualTrades = includeVirtualTrades;
            Depth = depth;
        }

        public DealsSubscription(Security security) : this(security, false, 0)
        {

        }
    }
}
