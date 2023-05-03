using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace AlorClient.Example
{
    internal class SimpleSubscription
    {
        private readonly ISecurities securities;
        private readonly ISubscriber subscriber;
        private readonly OptionsBoardFactory optionsBoardFactory;

        public SimpleSubscription(ISecurities securities, ISubscriber subscriber, OptionsBoardFactory optionsBoardFactory)
        {
            this.securities = securities;
            this.subscriber = subscriber;
            this.optionsBoardFactory = optionsBoardFactory;
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

        public void Subscribe(ISecurity security)
        {
            subscriber.Subscribe(new OrderBookSubscription(security, 20));
            subscriber.Subscribe(new DealsSubscription(security, true, 10));
        }
        private async Task<ISecurity> GetShareAsync()
        {
            var share = await securities.GetShareAsync("SBER");

            return share;
        }

        private async Task<ISecurity> GetFutureAsync(Task<ISecurity> share)
        {
            return await securities.GetFuturesAsync(await share)
                         .Where(x => x.Cancellation > DateTime.Now)
                         .OrderBy(x => x.Cancellation)
                         .FirstAsync();
        }

        private async Task<OptionsBoardItem> GetOptionsBoardAsync(Task<ISecurity> share)
        {
            var lastDeal = await securities.GetLastDealAsync(await share);
            var options = await securities.GetOptionsAsync(await share).ToArrayAsync();
            var OptionsBoard = optionsBoardFactory.Factory(options)
                       .Where(optionsBoard => optionsBoard.ExpirationDate > DateTime.Now)
                       .OrderBy(optionsBoard => optionsBoard.ExpirationDate)
                       .First();

            return OptionsBoard
                .Items
                .OrderBy(item => Math.Abs(item.Strike - lastDeal?.Price ?? 0))
                .First();
        }

    }
}
