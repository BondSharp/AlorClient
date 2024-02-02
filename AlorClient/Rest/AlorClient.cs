namespace AlorClient;

internal class AlorClient : IAlorClient
{
    private readonly Securities securities;
    private readonly Deals deals;

    public AlorClient(Securities securities, Deals deals)
    {
        this.securities = securities;
        this.deals = deals;
    }

    public IAsyncEnumerable<Deal> GetAllDeals(Security security, int batch, Deal? lastDeal) => deals.GetAllDeals(security, batch, lastDeal);
    public IAsyncEnumerable<Deal> GetHistoryDeals(Security security, int batch, DateTimeOffset? dateTime) => deals.GetHistoryDeals(security, batch, dateTime);
    public IAsyncEnumerable<Security> GetSecurities(TimeSpan duration) => securities.GetSecurities(duration);

}
