using AlorClient.Domain;
using System.Text.Json.Serialization;

namespace AlorClient
{
    public class OfferDto 
    {
        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("volume")]
        public int Volume { get; set; }
    }
}
