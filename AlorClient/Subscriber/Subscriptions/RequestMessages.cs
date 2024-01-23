
namespace AlorClient;
public class RequestMessages
{

    private readonly Security[] securities;

    public int OrderBookDepth { get; init; } = 20;

    public bool DealRequest { get; init ; } = true;

    public int DealDepth { get; init; } = 0;

    public int Frequency { get; init; }

    public RequestMessages(params Security[] securities)
    {
        this.securities = securities;
    }

    internal IEnumerable<SecuritySubscription> GetSubscriptions()
    {
        foreach (var security in securities) {
            if (OrderBookDepth > 0)
            {
                yield return new OrderBookSubscription(security, OrderBookDepth, Frequency);
            }
            if (DealRequest)
            {
                yield return new DealsSubscription(security, DealDepth, Frequency);
            }           
        }
    }
}
