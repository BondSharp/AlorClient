namespace AlorClient;
public interface IDeals
{
    IAsyncEnumerable<Deal> GetAllDeal(Security security, int batch, Deal? lastDeal);
}
