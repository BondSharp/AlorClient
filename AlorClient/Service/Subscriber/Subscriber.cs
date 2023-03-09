
namespace AlorClient
{
    internal class Subscriber : ISubscriber
    {
        private readonly SubscriptionSender subscriptionSender;
        private readonly SubscriptionCollection subscriptionCollection;
        public IDataProvider DataProvider { get; }

        public Subscriber(SubscriptionSender subscriptionSender, SubscriptionCollection subscriptionCollection, DataProvider dataProvider)
        {
            this.subscriptionSender = subscriptionSender;
            this.subscriptionCollection = subscriptionCollection;
            DataProvider = dataProvider;
            dataProvider.Reconnects.Subscribe(OnRecontion);
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
            subscriptionCollection.ClearCache();
            foreach (var subscription in subscriptionCollection.subscriptions)
            {
                subscriptionSender.Send(subscription);
            }
        }
    }
}
