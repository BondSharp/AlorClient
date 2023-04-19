using System.Text.Json.Serialization;

namespace AlorClient
{
    public sealed class OrderBookSubscription : SecuritySubscription
    {
        [JsonPropertyName("depth")]
        public int Depth { get; }

        public OrderBookSubscription(Security security, int depth) : base(security, "OrderBookGetAndSubscribe")
        {
            Depth = depth;
        }
    }
}
