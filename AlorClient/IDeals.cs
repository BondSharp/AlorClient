namespace AlorClient;
public interface IDeals
{
    IAsyncEnumerable<Deal> GetAllDeals(Security security, int batch, Deal? lastDeal);
    IAsyncEnumerable<Deal> GetHistoryDeals(Security security, int batch, DateTimeOffset? dateTime);
}
