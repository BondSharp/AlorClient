using System.Reactive.Linq;
using AlorClient.Service.WebSocket.DataProviders;
using Microsoft.Extensions.DependencyInjection;

namespace AlorClient;
internal class MarkerDataBuilder : IMarkerDataBuilder
{
    private readonly IServiceProvider serviceProvider;
    private readonly List<SecuritySubscription> securitySubscriptions = new List<SecuritySubscription>();
    public MarkerDataBuilder(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }
    public IObservable<SecurityMessage> Build()
    {
        var subscriptions = securitySubscriptions.ToArray();
        return Observable.Create<SecurityMessage>(observer =>
        {
            var scope = serviceProvider.CreateScope();
            var messageProvider = scope.ServiceProvider.GetRequiredService<MessageProvider>();
            messageProvider.Subscribe(observer);

            var subscriber = scope.ServiceProvider.GetRequiredService<Subscriber>();
            foreach (var subscription in subscriptions)
            {
                subscriber.Subscribe(subscription);
            }
            return scope;
        });      
    }

    public IMarkerDataBuilder OnOrderBook(Security security, int depth, int frequency)
    {
        securitySubscriptions.Add(new OrderBookSubscription(security, depth, frequency));

        return this;
    }

    public IMarkerDataBuilder OnDeals(Security security, int depth, int frequency)
    {
        securitySubscriptions.Add(new DealsSubscription(security, depth, frequency));

        return this;
    }
}
