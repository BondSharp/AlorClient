using AlorClient.Domain;
using System.Linq;

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

        public void Subscribe(Instrument instrument)
        {
            subscriber.Subscribe(new OrderBookSubscription(instrument, 20));
            subscriber.Subscribe(new DealsSubscription(instrument, true, 10));
        }
        private async Task<Instrument> GetShareAsync()
        {
            var share = await securities.GetAsync("SBER");

            return share;
        }

        private async Task<Instrument> GetFutureAsync()
        {
            return (await securities.GetFuturesAsync("SP"))
                         .Where(x => x.Cancellation > DateTime.Now)
                         .OrderBy(x => x.Cancellation)
                         .First();
        }

    }
}
