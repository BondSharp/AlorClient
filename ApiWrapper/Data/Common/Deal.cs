using ApiWrapper.JsonConverter;
using System.Text.Json.Serialization;

namespace ApiWrapper
{
    public class Deal
    {
        [JsonPropertyName("id")]
        public long id { get; set; }

        [JsonPropertyName("existing")]
        public bool existing { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("qty")]
        public int Quantity { get; set; }

        [JsonPropertyName("oi")]
        public int OpenInterest { get; set; }

        [JsonPropertyName("side")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Side Side { get; set; }

        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(TimestampJsonConverter))]
        public DateTimeOffset Timestamp { get; set; }

    }
}
