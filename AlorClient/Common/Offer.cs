using Data;
using System.Text.Json.Serialization;

namespace AlorClient
{
    public class Offer : IOffer
    {
        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("volume")]
        public int Volume { get; set; }
    }
}
