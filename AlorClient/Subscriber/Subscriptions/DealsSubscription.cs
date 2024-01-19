using System.Text.Json.Serialization;
using AlorClient.Domain;

namespace AlorClient;

public sealed class DealsSubscription : SecuritySubscription
{
    [JsonPropertyName("includeVirtualTrades")]
    public bool IncludeVirtualTrades { get; }

    [JsonPropertyName("depth")]
    public int Depth { get; }

    public DealsSubscription(Instrument instrument, bool includeVirtualTrades, int depth) : base(instrument, "AllTradesGetAndSubscribe")
    {
        IncludeVirtualTrades = includeVirtualTrades;
        Depth = depth;
    }

    public DealsSubscription(Instrument security) : this(security, false, 0)
    {

    }
}
