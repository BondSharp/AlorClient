using System.Text.Json.Serialization;
using Common;

namespace AlorClient
{
    public class OrderBook : IOrderBook
    {
        [JsonPropertyName("bids")]
        public required  Offer[] Bids { get; set; }

        [JsonPropertyName("asks")]
        public required Offer[] Asks { get; set; }

        [JsonPropertyName("ms_timestamp")]
        [JsonConverter(typeof(TimestampJsonConverter))]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("existing")]
        public bool Existing { get; set; }
        public DateTimeOffset ClientTimestamp { get; set; } = DateTimeOffset.UtcNow;

        IOffer[] IOrderBook.Bids => Bids;

        IOffer[] IOrderBook.Asks => Asks;
    }
}
