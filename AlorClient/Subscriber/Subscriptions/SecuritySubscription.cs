using System.Text.Json.Serialization;
using Data;

namespace AlorClient
{
    public abstract class SecuritySubscription : Subscription
    {
        [JsonIgnore]
        public ISecurity Security { get; }

        [JsonPropertyName("code")]
        public string Code => Security.Symbol;

        [JsonPropertyName("exchange")]
        public string Exchange => Security.Exchange;

        protected SecuritySubscription(ISecurity security, string operationCode) : base(operationCode)
        {
            Security = security;
        }


    }
}
