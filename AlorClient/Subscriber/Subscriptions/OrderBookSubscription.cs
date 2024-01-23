using System.Text.Json.Serialization;
using AlorClient.Domain;

namespace AlorClient;

internal sealed class OrderBookSubscription : SecuritySubscription
{
    [JsonPropertyName("depth")]
    public int Depth { get; }

    public OrderBookSubscription(Security instrument, int depth, int frequency) : base(instrument, "OrderBookGetAndSubscribe", frequency)
    {
        Depth = depth;
    }
}
