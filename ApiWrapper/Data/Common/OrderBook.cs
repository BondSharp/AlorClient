using ApiWrapper.JsonConverter;
using System.Text.Json.Serialization;

namespace ApiWrapper
{
    public class OrderBook
    {
        [JsonPropertyName("bids")]
        public required  Offer[] Bids { get; set; }

        [JsonPropertyName("asks")]
        public required Offer[] Asks { get; set; }

        [JsonPropertyName("ms_timestamp")]
        [JsonConverter(typeof(TimestampJsonConverter))]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("existing")]
        public bool existing { get; set; }
    }
}
