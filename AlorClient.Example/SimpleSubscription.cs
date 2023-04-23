using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var future = GetFutureAsync(share);
            var optionsBoard = await GetOptionsBoardAsync(share);
            Subscribe(await share);
            Subscribe(await future);
            Subscribe(optionsBoard.Put);
            Subscribe(optionsBoard.Call);
        }

        public void Subscribe(Security security)
        {
            subscriber.Subscribe(new OrderBookSubscription(security, 20));
            subscriber.Subscribe(new DealsSubscription(security,true,10));
        }
        private async Task<Share> GetShareAsync()
        {
            var share = await securities.GetShareAsync("SBER");

            return share;
        }

        private async Task<Future> GetFutureAsync(Task<Share> share)
        {
            return await securities.GetFuturesAsync(await share)
                         .Where(x => x.ExpirationDate > DateTime.Now)
                         .OrderBy(x => x.ExpirationDate)
                         .FirstAsync();
        }

        private async Task<OptionsBoard> GetOptionsBoardAsync(Task<Share> share)
        {
            var lastDeal = await securities.GetLastDealAsync(await share);

            var OptionsBoard = await securities.GetOptionsBoardsAsync(await share)
                       .Where(optionsBoard => optionsBoard.ExpirationDate > DateTime.Now)
                       .OrderBy(optionsBoard => optionsBoard.ExpirationDate)
                       .ThenBy(optionsBoard => Math.Abs(optionsBoard.Strike - lastDeal?.Price ?? 0))
                       .FirstAsync();

            return OptionsBoard;
        }

    }
}
