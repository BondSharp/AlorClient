using Microsoft.Extensions.Hosting;
using ApiWrapper;
using ApiWrapper.Example;
using Microsoft.Extensions.DependencyInjection;

namespace ApiWrapper.App
{
    internal class Example : BackgroundService
    {
        private readonly ISecurities securities;
        private readonly ISubscriber subscriber;
        private readonly IServiceProvider serviceProvider;

        public Example(ISecurities securities, ISubscriber subscriber, IServiceProvider serviceProvider)
        {
            this.securities = securities;
            this.subscriber = subscriber;
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            subscriber.Messages.Subscribe(OnMessage);
            subscriber.Notifications.Subscribe(OnNotification);



            var share = await securities.GetShareAsync("SBER");
            var optionFinder = serviceProvider.CreateOptionFinder(share, stoppingToken);
            var optionPut = optionFinder.FindOptionPut();
            var optionCall = optionFinder.FindOptionCall();
            var future = securities.GetFuturesAsync(share)
                .Where(x => x.ExpirationDate > DateTimeOffset.Now.Date)
                .OrderBy(x => x.ExpirationDate)
                .FirstAsync(stoppingToken);

            Subscribe(share);
            Subscribe(await future);
            Subscribe(await optionPut);
            Subscribe(await optionCall);
        }




        public void Subscribe(Security? security)
        {
            if (security != null)
            {
                subscriber.Subscribe(new OrderBookSubscription(security, 3));
                subscriber.Subscribe(new DealsSubscription(security));
            }
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
