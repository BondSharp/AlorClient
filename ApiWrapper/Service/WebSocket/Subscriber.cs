using ApiWrapper.Service.WebSocket;
using System.Reactive.Linq;
using System.Text.Json;
using Websocket.Client;
using Websocket.Client.Models;

namespace ApiWrapper
{
    internal class Subscriber : ISubscriber
    {
        private readonly SubscriptionSender subscriptionSender;
        private readonly SubscriptionCollection subscriptionCollection;
        public IObservable<Message> Messages { get; }

        public IObservable<Notification> Notifications { get; }

        public Subscriber(SubscriptionSender subscriptionSender, MessageProvider observableMessage, NotificationProvider notificationProvider, SubscriptionCollection subscriptionCollection)
        {
            Messages = observableMessage;
            Notifications = notificationProvider;
            this.subscriptionSender = subscriptionSender;
            this.subscriptionCollection = subscriptionCollection;
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

    }
}
