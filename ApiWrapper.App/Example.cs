using ApiWrapper.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper.App
{
    internal class Example : BackgroundService
    {
        private readonly SecuritiesIntarface securities;
        private readonly SubscriberIntarface subscriber;

        public Example(SecuritiesIntarface securities, SubscriberIntarface subscriber)
        {
            this.securities = securities;
            this.subscriber = subscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            subscriber.Messages.Subscribe(OnMessage);

            subscriber.Notifications.Subscribe(OnNotification);

            var sberbankShare = await securities.Get<Share>("SBER").FirstAsync();

            var orderBookSubscribe = new OrderBookSubscription(sberbankShare, 10);
            subscriber.Subscribe(orderBookSubscribe);
        }


        private void OnMessage(Message message)
        {
            Console.WriteLine(message);
        }

        private void OnNotification(Notification notification)
        {
            Console.WriteLine(notification);
        }





    }
}
