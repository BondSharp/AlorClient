
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
            if (subscriptions.Add(subscription))
            {
                if (!cache.ContainsValue(subscription))
                {
                    cache.Add(subscription.Guid, subscription);
                }
                return true;
            }
            return false;
        }

        public bool Remove(Subscription subscription)
        {
            return subscriptions.Remove(subscription);
        }

        public void ClearCache()
        {
            cache.Clear();
            foreach (var subscription in subscriptions)
            {
                cache.Add(subscription.Guid, subscription);
            }
        }
    }
}
