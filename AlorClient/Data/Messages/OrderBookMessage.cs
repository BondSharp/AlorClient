namespace AlorClient
{
    public class OrderBookMessage : SecurityMessage
    {
        public OrderBookSubscription OrderBookSubscription { get; }
        public OrderBook OrderBook { get; }
        public OrderBookMessage(OrderBookSubscription OrderBookSubscription, OrderBook orderBook) : base(OrderBookSubscription)
        {
            this.OrderBookSubscription = OrderBookSubscription;
            OrderBook = orderBook;
        }
    }
}
