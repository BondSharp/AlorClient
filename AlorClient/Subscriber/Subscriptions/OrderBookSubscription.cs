using System.Text.Json.Serialization;
using AlorClient.Domain;

namespace AlorClient
{
    public sealed class OrderBookSubscription : SecuritySubscription
    {
        [JsonPropertyName("depth")]
        public int Depth { get; }

        public OrderBookSubscription(Instrument instrument, int depth) : base(instrument, "OrderBookGetAndSubscribe")
        {
            Depth = depth;
        }
    }
}
