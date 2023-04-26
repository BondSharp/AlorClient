using System.Text.Json.Serialization;
using Data;

namespace AlorClient
{
    public sealed class OrderBookSubscription : SecuritySubscription
    {
        [JsonPropertyName("depth")]
        public int Depth { get; }

        public OrderBookSubscription(ISecurity security, int depth) : base(security, "OrderBookGetAndSubscribe")
        {
            Depth = depth;
        }
    }
}
