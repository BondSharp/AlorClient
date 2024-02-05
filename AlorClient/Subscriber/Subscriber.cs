using System.Reactive.Linq;
using Websocket.Client;
namespace AlorClient;

internal class Subscriber : IDisposable, IObservable<Message>
{
    private readonly SubscriptionSender subscriptionSender;
    private readonly SubscriptionCollection subscriptionCollection;
    private readonly IObservable<Message> messages;
    private readonly IDisposable disposable;
    public Subscriber(SubscriptionSender subscriptionSender, SubscriptionCollection subscriptionCollection, ReconnectProvider reconnect, MarkerDataProvider markerDataProvider)
    {
        this.subscriptionSender = subscriptionSender;
        this.subscriptionCollection = subscriptionCollection;
        messages = markerDataProvider.AsObservable().Concat(reconnect.AsObservable());
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
        if (reconnect.ReconnectionType != ReconnectionType.Initial)
        {
            foreach (var subscription in subscriptionCollection.GetSubscriptions())
            {
                subscriptionSender.Send(subscription);
            }
        }

    }

    public void Dispose()
    {
        disposable?.Dispose();
    }

    public IDisposable Subscribe(IObserver<Message> observer)
    {
        return messages.Where(message =>
         {
             if (message is Notification notification)
             {
                 if (notification.Code != 200)
                 {
                     throw new Exception(notification.ToString());
                 }
                 return false;
             }
             return true;
         }).Subscribe(observer);
    }
}
