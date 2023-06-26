using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlorClient.Example
{
    public class SimpleEchoMessage
    {
        private readonly ISubscriber subscriber;

        public SimpleEchoMessage(ISubscriber subscriber)
        {
            this.subscriber = subscriber;
        }

        public IDisposable Echo()
        {
            return subscriber
                .DataProvider
                .Messages.OfType<SecurityMessage>()
                .Subscribe(OnMessage);
        }

        private void OnMessage(SecurityMessage securityMessage)
        {
            string? textMessage = null;
            var symbol = securityMessage.SecuritySubscription.Code;

            if (securityMessage is DealMessage dealMessage)
            {
                textMessage = $"deal with price {dealMessage.Deal.Price}";
            }

            if (securityMessage is OrderBookMessage orderBookMessage)
            {
                var ask = orderBookMessage.OrderBook.Asks
                    .Select(ask => ask.Price)
                    .OrderByDescending(price => price)
                    .FirstOrDefault();

                var bid = orderBookMessage.OrderBook.Bids
                   .Select(ask => ask.Price)
                   .OrderBy(price => price)
                   .FirstOrDefault();

                textMessage = $"orderBook with ask {ask} and bid {bid}";
            }

            Console.WriteLine($"Received a message '{textMessage}' for {symbol}");
        }
    }
}
