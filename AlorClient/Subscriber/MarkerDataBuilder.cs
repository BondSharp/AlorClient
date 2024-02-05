using System.Reactive.Linq;
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
    public IObservable<Message> Build()
    {
        var subscriptions = securitySubscriptions.ToArray();
        return Observable.Create<Message>(observer =>
        {
            var scope = serviceProvider.CreateScope();

            var subscriber = scope.ServiceProvider.GetRequiredService<Subscriber>();
            subscriber.Subscribe(observer);
            foreach (var subscription in subscriptions)
            {
                subscriber.Subscribe(subscription);
            }
            return scope;
        });
    }

    public IMarkerDataBuilder OrderBook(Security security, int depth, int frequency)
    {
        securitySubscriptions.Add(new OrderBookSubscription(security, depth, frequency));

        return this;
    }

    public IMarkerDataBuilder Deals(Security security, int depth, int frequency)
    {
        securitySubscriptions.Add(new DealsSubscription(security, depth, frequency));

        return this;
    }
}
