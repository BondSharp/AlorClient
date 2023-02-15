using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;
using Websocket.Client.Models;

namespace ApiWrapper
{
    internal class WebsocketReconnect : IDisposable
    {
        private readonly SubscriptionSender subscriptionSender;
        private readonly SubscriptionCollection subscriptionCollection;
        private readonly IDisposable disposable;

        public WebsocketReconnect(IWebsocketClient client, SubscriptionSender subscriptionSender, SubscriptionCollection subscriptionCollection)
        {
            this.subscriptionSender = subscriptionSender;
            this.subscriptionCollection = subscriptionCollection;
            disposable = client.ReconnectionHappened.Subscribe(Reconnection);
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }

        private void Reconnection(ReconnectionInfo obj)
        {
            if (obj.Type != ReconnectionType.Initial)
            {
                subscriptionCollection.ClearCache();
                foreach (var subscription in subscriptionCollection.subscriptions)
                {
                    subscriptionSender.Send(subscription);
                }
            }
        }
    }
}
