
namespace AlorClient;

internal class Subscriber : IDisposable
{
    private readonly SubscriptionSender subscriptionSender;
    private readonly SubscriptionCollection subscriptionCollection;
    private readonly IDisposable disposable;
    public Subscriber(SubscriptionSender subscriptionSender, SubscriptionCollection subscriptionCollection, ReconnectProvider reconnect)
    {
        this.subscriptionSender = subscriptionSender;
        this.subscriptionCollection = subscriptionCollection;
        disposable = reconnect.Subscribe(OnRecontion);
    }

    public void Subscribe(SecuritySubscription securitySubscription)
    {
        if (subscriptionCollection.Add(securitySubscription))
        {
            subscriptionSender.Send(securitySubscription);
        };
    }

    private void OnRecontion(Reconnect reconnect)
    {
        foreach (var subscription in subscriptionCollection.GetSubscriptions())
        {
            subscriptionSender.Send(subscription);
        }
    }

    public void Dispose()
    {
        disposable?.Dispose();
    }
}
