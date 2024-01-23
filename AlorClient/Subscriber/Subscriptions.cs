using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using AlorClient.Service.WebSocket.DataProviders;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace AlorClient;
internal class Subscriptions : ISubscriptions
{
    private readonly IServiceProvider serviceProvider;

    public Subscriptions(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }
    public IObservable<Message> CreateMessages(RequestMessages request)
    {
        return Observable.Create<Message>(observer =>
        {
            var scope = serviceProvider.CreateScope();
            var messageProvider = scope.ServiceProvider.GetRequiredService<MessageProvider>();
            messageProvider.Subscribe(observer);

            var subscriber = scope.ServiceProvider.GetRequiredService<Subscriber>();
            foreach (var subscription in request.GetSubscriptions())
            {
                subscriber.Subscribe(subscription);
            }
            return scope;
        });

      
    }
}
