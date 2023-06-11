using System.Text.Json.Serialization;
using Common;

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
