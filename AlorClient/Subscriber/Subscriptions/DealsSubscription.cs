using System.Text.Json.Serialization;
using AlorClient.Domain;

namespace AlorClient;

internal sealed class DealsSubscription : SecuritySubscription
{
    [JsonPropertyName("includeVirtualTrades")]
    public bool IncludeVirtualTrades { get; } = false;

    [JsonPropertyName("depth")]
    public int Depth { get; }

    public DealsSubscription(Security instrument, int depth,int frequency) : base(instrument, "AllTradesGetAndSubscribe", frequency)
    {
        Depth = depth;
    }
}
