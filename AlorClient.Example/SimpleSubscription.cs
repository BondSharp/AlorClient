using Common;

namespace AlorClient.Example
{
    internal class SimpleSubscription
    {
        private readonly ISecurities securities;
        private readonly ISubscriber subscriber;

        public SimpleSubscription(ISecurities securities, ISubscriber subscriber)
        {
            this.securities = securities;
            this.subscriber = subscriber;
        }
        public async void Subscribe()
        {
            var share = GetShareAsync();
            var future = GetFutureAsync();
            Subscribe(await share);
            Subscribe(await future);
        }

        public void Subscribe(ISecurity security)
        {
            subscriber.Subscribe(new OrderBookSubscription(security, 20));
            subscriber.Subscribe(new DealsSubscription(security, true, 10));
        }
        private async Task<ISecurity> GetShareAsync()
        {
            var share = await securities.GetAsync("SBER");

            return share;
        }

        private async Task<ISecurity> GetFutureAsync()
        {
            return (await securities.GetFuturesAsync("SP"))
                         .Where(x => x.Cancellation > DateTime.Now)
                         .OrderBy(x => x.Cancellation)
                         .First();
        }

    }
}
