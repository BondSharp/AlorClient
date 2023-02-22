using System.Text.Json.Serialization;

namespace ApiWrapper
{
    public abstract class Security
    {
        public required string Symbol { get; set; }

        public required string Shortname { get; set; }

        public required string Description { get; set; }
        public required string Type { get; set; }

        public required string Exchange { get; set; }

        public required string CfiCode { get; set; }

        public required string Board { get; set; }

        [JsonPropertyName("cancellation")]
        public required DateTime ExpirationDate { get; set; }
    }
}
