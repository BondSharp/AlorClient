using System.Text.Json.Serialization;

namespace AlorClient
{
    public class Option : Security
    {
        [JsonPropertyName("cancellation")]
        public required DateTime ExpirationDate { get; set; }

        [JsonIgnore]
        public double Strike { get; set; }

        [JsonIgnore]
        public OptionType OptionType { get; set; }

    }
}
