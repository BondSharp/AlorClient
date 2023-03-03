using System.Text.Json.Serialization;

namespace ApiWrapper
{
    public class Future : Security
    {

        [JsonPropertyName("cancellation")]
        public DateTime ExpirationDate { get; set; }
        
    }
}
