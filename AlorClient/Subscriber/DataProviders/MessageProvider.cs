using System.Reactive.Linq;
using System.Text.Json;
using Websocket.Client;
using static System.Net.Mime.MediaTypeNames;

namespace AlorClient.Service.WebSocket.DataProviders;

internal class MessageProvider : IObservable<SecurityMessage>
{
    private readonly SubscriptionCollection subscriptionCollection;
    private readonly IWebsocketClient client;

    public MessageProvider(SubscriptionCollection SubscriptionCollection, IWebsocketClient client)
    {
        subscriptionCollection = SubscriptionCollection;
        this.client = client;
    }

    public IDisposable Subscribe(IObserver<SecurityMessage> observer)

    {
        return client.MessageReceived
            .Select(Parse)
            .OfType<SecurityMessage>()
            .Subscribe(observer);
    }

    private Message Parse(ResponseMessage responseMessage)
    {
        if (responseMessage.Text.StartsWith("{ \"data"))
        {
            ParseMessage(responseMessage);
            return ParseMessage(responseMessage);
        }
        if (responseMessage.Text.StartsWith("{\"requestGuid"))
        {
            var notification = ParseNotification(responseMessage);
            if (notification.Code != 200)
            {
                throw new Exception(notification.ToString());
            }
            return notification;
        }

        throw new ArgumentException(nameof(responseMessage));
    }

    private Message ParseMessage(ResponseMessage responseMessage)
    {
        using (var jsonDocument = JsonDocument.Parse(responseMessage.Text))
        {

            var data = jsonDocument.RootElement.GetProperty("data");
            var guid = jsonDocument.RootElement.GetProperty("guid").GetGuid();
            var subscription = subscriptionCollection.GetSubscription(guid);
            var message = ParseMessage(subscription, data);

            return message;
        }
    }

    private Message ParseMessage(Subscription subscription, JsonElement data)
    {
        if (subscription is OrderBookSubscription bookSubscription)
        {
            var orderBook = Deserialize<OrderBook>(data);
            return new OrderBookMessage(bookSubscription.Security, orderBook);
        }

        if (subscription is DealsSubscription allDealsSubscription)
        {
            var deal = Deserialize<Deal>(data);
            return new DealMessage(allDealsSubscription.Security, deal);
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

    private Notification ParseNotification(ResponseMessage responseMessage)
    {
        using (var jsonDocument = JsonDocument.Parse(responseMessage.Text))
        {
            var guid = jsonDocument.RootElement.GetProperty("requestGuid").GetGuid();
            var code = jsonDocument.RootElement.GetProperty("httpCode").GetInt32();
            var message = jsonDocument.RootElement.GetProperty("message").GetString() ?? throw new NullReferenceException();
            var subscription = subscriptionCollection.GetSubscription(guid);

            return new Notification(code, message, subscription);
        }
    }
}
