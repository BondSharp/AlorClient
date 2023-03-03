using System.Text.Json.Serialization;

namespace ApiWrapper
{
    internal sealed class UnSubscription : Subscription
    {
        [JsonIgnore]
        public Subscription Subscription { get; }

        public UnSubscription(Subscription subscription) : base("unsubscribe", subscription.Guid)
        {
            Subscription = subscription;
        }


    }
}
