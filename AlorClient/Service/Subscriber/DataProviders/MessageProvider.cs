using System.Reactive.Linq;
using System.Text.Json;
using Websocket.Client;

namespace AlorClient.Service.WebSocket.DataProviders
{
    internal class MessageProvider : IObservable<Message>
    {
        private readonly SubscriptionCollection subscriptionCollection;
        private readonly IWebsocketClient client;

        public MessageProvider(SubscriptionCollection SubscriptionCollection, IWebsocketClient client)
        {
            subscriptionCollection = SubscriptionCollection;
            this.client = client;
        }

        public IDisposable Subscribe(IObserver<Message> observer)
        {
            return client.MessageReceived
                .Where(x => x.Text.StartsWith("{ \"data"))
                .Select(Parser)
                .Subscribe(observer);
        }

        private Message Parser(ResponseMessage obj)
        {
            using (var jsonDocument = JsonDocument.Parse(obj.Text))
            {

                var data = jsonDocument.RootElement.GetProperty("data");
                var guid = jsonDocument.RootElement.GetProperty("guid").GetGuid();
                var subscription = subscriptionCollection.GetSubscription(guid);
                var message = Parse(subscription, data);

                return message;
            }
        }

        private Message Parse(Subscription subscription, JsonElement data)
        {
            if (subscription is OrderBookSubscription bookSubscription)
            {
                var orderBook = Deserialize<OrderBook>(data);
                return new OrderBookMessage(bookSubscription, orderBook);
            }

            if (subscription is DealsSubscription allDealsSubscription)
            {
                var deal = Deserialize<Deal>(data);
                return new DealMessage(allDealsSubscription, deal);
            }

            throw new ArgumentException(nameof(subscription));
        }

        private T Deserialize<T>(JsonElement jsonElement)
        {
            var result = jsonElement.Deserialize<T>();
            if (result == null)
            {
                throw new ArgumentException(nameof(result));
            }
            return result;
        }
    }
}
