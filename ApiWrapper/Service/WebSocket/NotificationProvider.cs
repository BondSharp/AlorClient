using ApiWrapper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Websocket.Client;

namespace ApiWrapper.Service.WebSocket
{
    internal class NotificationProvider : IObservable<Notification>
    {
        private readonly SubscriptionCollection subscriptionCollection;
        private readonly IWebsocketClient client;

        public NotificationProvider(SubscriptionCollection SubscriptionCollection, IWebsocketClient client)
        {
            subscriptionCollection = SubscriptionCollection;
            this.client = client;
        }

        public IDisposable Subscribe(IObserver<Notification> observer)
        {
            return client.MessageReceived
                .Where(message => message.Text.StartsWith("{\"requestGuid"))
                .Select(Parse)
                .Subscribe(observer);
        }


        private Notification Parse(ResponseMessage obj)
        {
            using (var jsonDocument = JsonDocument.Parse(obj.Text))
            {
                var guid = jsonDocument.RootElement.GetProperty("requestGuid").GetGuid();
                var code = jsonDocument.RootElement.GetProperty("httpCode").GetInt32();
                var message = jsonDocument.RootElement.GetProperty("message").GetString();
                var subscription = subscriptionCollection.GetSubscription(guid);

                return new Notification(code, message, subscription);
            }
        }

    }
}
