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
    }
}
