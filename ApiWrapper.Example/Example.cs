using Microsoft.Extensions.Hosting;

namespace ApiWrapper.Example
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

            var nowDate = DateTimeOffset.Now.Date;

            var share = await securities.GetShareAsync("SBER");
            var lastDeal = securities.GetLastDealAsync(share);

            var future = securities.GetFuturesAsync(share)
               .Where(x => x.ExpirationDate > nowDate)
               .OrderBy(x => x.ExpirationDate)
               .FirstAsync(stoppingToken);

            var optionsBoard = securities.GetOptionsBoardsAsync(share)
                  .Where(x => x.ExpirationDate > nowDate)
                  .FirstAsync(stoppingToken);

            Subscribe(share);
            Subscribe(await future);
            Subscribe(await optionsBoard, await lastDeal);
        }
        public void Subscribe(OptionsBoard? optionsBoard, Deal? lastDeal)
        {
            if (lastDeal != null)
            {
                var call = optionsBoard?.Calls
                    .Where(call => call.Strike > lastDeal.Price)
                    .OrderBy(call => call.Strike)
                    .FirstOrDefault();
                var put = optionsBoard?.Puts
                    .Where(call => call.Strike < lastDeal.Price)
                    .OrderByDescending(call => call.Strike)
                    .FirstOrDefault();

                Subscribe(call);
                Subscribe(put);
            }
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

            if (message is SecurityMessage securityMessage)
            {
                var symbol = securityMessage.SecuritySubscription.Security.Symbol;
                var textMessage = GetTextMessage(securityMessage);
                Console.WriteLine($"Received a message '{textMessage}' for {symbol}");
            }
        }

        private string? GetTextMessage(SecurityMessage securityMessage)
        {
            if (securityMessage is DealMessage dealMessage)
            {
                return $"deal with price {dealMessage.Deal.Price}";
            }

            if (securityMessage is OrderBookMessage orderBookMessage)
            {
                var ask = orderBookMessage.OrderBook.Asks
                    .Select(ask => ask.Price)
                    .OrderBy(price => price)
                    .FirstOrDefault();

                var bid = orderBookMessage.OrderBook.Bids
                   .Select(ask => ask.Price)
                   .OrderBy(price => price)
                   .FirstOrDefault();

                return $"orderBook with ask {ask} and bid {bid}";
            }
            return null;
        }

        private void OnNotification(Notification notification)
        {
            Console.WriteLine(notification);
        }
    }
}
