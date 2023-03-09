using System.Text.Json.Serialization;

namespace AlorClient
{
    public abstract class SecuritySubscription : Subscription
    {
        [JsonIgnore]
        public Security Security { get; }

        [JsonPropertyName("code")]
        public string Code => Security.Symbol;

        [JsonPropertyName("exchange")]
        public string Exchange => Security.Exchange;

        protected SecuritySubscription(Security security, string operationCode) : base(operationCode)
        {
            Security = security;
        }


    }
}
