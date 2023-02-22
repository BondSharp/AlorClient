using Microsoft.Extensions.Hosting;

namespace ApiWrapper.App
{
    internal class Example : BackgroundService
    {
        private readonly ISecurities securities;
        private readonly ISubscriber subscriber;

        public Example(ISecurities securities, ISubscriber subscriber)
        {
            this.securities = securities;
            this.subscriber = subscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            subscriber.Messages.Subscribe(OnMessage);
            subscriber.Notifications.Subscribe(OnNotification);

            var share = securities.GetShareAsync("SBER");

            var future = securities.GetFuturesAsync(await share)
                .Where(x => x.ExpirationDate > DateTimeOffset.Now.Date)
                .OrderBy(x => x.ExpirationDate)
                .FirstAsync(stoppingToken);
            
            Subscribe(await share);
            Subscribe(await future);
        }

        public void Subscribe(Security security)
        {
            subscriber.Subscribe(new OrderBookSubscription(security, 3));
            subscriber.Subscribe(new DealsSubscription(security));
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
