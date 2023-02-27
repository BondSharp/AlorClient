using ApiWrapper.JsonConverter;
using System.Text.Json.Serialization;

namespace ApiWrapper
{
    public class OrderBook
    {
        [JsonPropertyName("bids")]
        public  Offer[] Bids { get; set; }

        [JsonPropertyName("asks")]
        public  Offer[] Asks { get; set; }

        [JsonPropertyName("ms_timestamp")]
        [JsonConverter(typeof(TimestampJsonConverter))]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("existing")]
        public bool existing { get; set; }
    }
}
