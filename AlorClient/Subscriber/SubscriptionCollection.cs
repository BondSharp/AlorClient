
namespace AlorClient;

internal class SubscriptionCollection
{
    private readonly Dictionary<Guid, Subscription> subscriptions;

    public SubscriptionCollection()
    {
        subscriptions = new Dictionary<Guid, Subscription>();
    }

    public Subscription GetSubscription(Guid guid)
    {
        return subscriptions[guid];
    }

    public Subscription[] GetSubscriptions() => subscriptions.Values.ToArray();

    public bool Add(Subscription subscription)
    {
        if (!this.subscriptions.ContainsKey(subscription.Guid))
        {
            this.subscriptions.Add(subscription.Guid, subscription);
            return true;
        }

        return false;
              
    }
}
