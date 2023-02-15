using System.Text.Json.Serialization;

namespace ApiWrapper
{
    public class Offer
    {
        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("volume")]
        public double Volume { get; set; }

    }
}
