using System.Reactive.Linq;
using System.Text.Json;
using Websocket.Client;
using static System.Net.Mime.MediaTypeNames;

namespace AlorClient.Service.WebSocket.DataProviders;

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
            .Select(Parse)
            .Subscribe(observer);
    }

    private Message Parse(ResponseMessage responseMessage)
    {
        if (responseMessage.Text.StartsWith("{ \"data"))
        {
            return ParseMessage(responseMessage);
        }
        if (responseMessage.Text.StartsWith("{\"requestGuid"))
        {
            return ParseNotification(responseMessage);
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
            var orderBook = Deserialize<OrderBookDto>(data);
            return new OrderBookMessage(bookSubscription, orderBook);
        }

        if (subscription is DealsSubscription allDealsSubscription)
        {
            var deal = Deserialize<DealDto>(data);
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
