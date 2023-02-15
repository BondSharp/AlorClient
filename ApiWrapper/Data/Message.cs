using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper.Data
{
    public abstract class Message
    {

    }

    public class DealMessage : Message
    {
        public AllDealsSubscription AllDealsSubscription { get; }
        public Deal Deal { get; }

        public DealMessage(AllDealsSubscription allDealsSubscription, Deal deal)
        {
            AllDealsSubscription = allDealsSubscription;
            Deal = deal;
        }


    }


    public class OrderBookMessage : Message
    {
        public OrderBookSubscription OrderBookSubscription { get; }
        public OrderBook OrderBook { get; }
        public OrderBookMessage(OrderBookSubscription OrderBookSubscription, OrderBook orderBook)
        {
            this.OrderBookSubscription = OrderBookSubscription;
            OrderBook = orderBook;
        }
    }


}
