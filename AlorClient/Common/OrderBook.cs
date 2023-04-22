using Data;
using System.Text.Json.Serialization;

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

        IOffer[] IOrderBook.Bids => Bids;

        IOffer[] IOrderBook.Asks => Asks;
    }
}
