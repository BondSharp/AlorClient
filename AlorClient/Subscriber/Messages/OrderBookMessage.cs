namespace AlorClient
{
    public class OrderBookMessage : SecurityMessage
    {
        public OrderBookSubscription OrderBookSubscription { get; }
        public OrderBookDto OrderBook { get; }
        public OrderBookMessage(OrderBookSubscription OrderBookSubscription, OrderBookDto orderBook) : base(OrderBookSubscription)
        {
            this.OrderBookSubscription = OrderBookSubscription;
            OrderBook = orderBook;
        }
    }
}
