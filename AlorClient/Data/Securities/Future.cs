using System.Text.Json.Serialization;

namespace AlorClient
{
    public class Future : Security
    {
        [JsonPropertyName("cancellation")]
        public DateTime ExpirationDate { get; set; }        
    }
}
