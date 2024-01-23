namespace AlorClient;

public class OrderBookMessage : SecurityMessage
{
    public OrderBook OrderBook { get; }
    public OrderBookMessage(Security security, OrderBook orderBook) : base(security)
    {
        OrderBook = orderBook;
    }

    public override string ToString()
    {
        return $"{base.ToString()} {OrderBook}";
    }
}
