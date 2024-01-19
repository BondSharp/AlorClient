
namespace AlorClient;

internal class Subscriber : ISubscriber, IDisposable
{
    private readonly SubscriptionSender subscriptionSender;
    private readonly SubscriptionCollection subscriptionCollection;
    private readonly IDisposable disposable;

    public IDataProvider DataProvider { get; }


    public Subscriber(SubscriptionSender subscriptionSender, SubscriptionCollection subscriptionCollection, DataProvider dataProvider)
    {
        this.subscriptionSender = subscriptionSender;
        this.subscriptionCollection = subscriptionCollection;
        DataProvider = dataProvider;
        disposable = dataProvider.Reconnects.Subscribe(OnRecontion);
    }

    public void Subscribe(Subscription subscription)
    {
        if (subscriptionCollection.Add(subscription))
        {
            subscriptionSender.Send(subscription);
        };
    }

    public void UnSubscribe(Subscription subscription)
    {
        if (subscriptionCollection.Remove(subscription))
        {
            subscriptionSender.Send(new UnSubscription(subscription));
        }
    }

    private void OnRecontion(Reconnect reconnect)
    {
        foreach (var subscription in subscriptionCollection.subscriptions)
        {
            subscriptionSender.Send(subscription);
        }
    }

    public void Dispose()
    {
        disposable?.Dispose();
    }
}
