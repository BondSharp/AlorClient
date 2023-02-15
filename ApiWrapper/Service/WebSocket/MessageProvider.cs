using ApiWrapper.Data;
using System.Reactive.Linq;
using System.Text.Json;
using Websocket.Client;

namespace ApiWrapper
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
                .Where(x=>x.Text.StartsWith("{ \"data"))
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
                var orderBook = data.Deserialize<OrderBook>();
                return new OrderBookMessage(bookSubscription, orderBook);
            }

            if (subscription is AllDealsSubscription allDealsSubscription)
            {
                var deal = data.Deserialize<Deal>(new JsonSerializerOptions() { });
                return new DealMessage(allDealsSubscription, deal);
            }

            throw new ArgumentException(nameof(subscription));
        }


    }
}
