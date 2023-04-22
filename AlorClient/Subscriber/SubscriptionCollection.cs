
namespace AlorClient
{
    internal class SubscriptionCollection
    {
        public readonly Dictionary<Guid, Subscription> cache;
        public readonly HashSet<Subscription> subscriptions;

        public SubscriptionCollection()
        {
            cache = new Dictionary<Guid, Subscription>();
            subscriptions = new HashSet<Subscription>();
        }

        public Subscription GetSubscription(Guid guid)
        {
            return cache[guid];
        }

        public Subscription[] GetSubscriptions() => subscriptions.ToArray();

        public bool Add(Subscription subscription)
        {
            if (!cache.ContainsKey(subscription.Guid))
            {
                cache.Add(subscription.Guid, subscription);
            }
            
            return subscriptions.Add(subscription);
                  
        }

        public bool Remove(Subscription subscription)
        {
            return subscriptions.Remove(subscription);
        }      
    }
}
