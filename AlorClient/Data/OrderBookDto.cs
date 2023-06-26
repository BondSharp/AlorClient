using System.Text.Json.Serialization;
using AlorClient.Domain;

namespace AlorClient
{
    public class OrderBookDto
    {
        [JsonPropertyName("bids")]
        public required  OfferDto[] Bids { get; set; }

        [JsonPropertyName("asks")]
        public required OfferDto[] Asks { get; set; }

        [JsonPropertyName("ms_timestamp")]
        [JsonConverter(typeof(TimestampJsonConverter))]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("existing")]
        public bool Existing { get; set; }
        public DateTimeOffset ClientTimestamp { get; set; } = DateTimeOffset.UtcNow;
    }
}
