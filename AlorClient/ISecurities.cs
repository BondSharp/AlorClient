using Data;

namespace AlorClient
{
    public interface ISecurities
    {
        Task<ISecurity> GetShareAsync(string symbol);
        IAsyncEnumerable<ISecurity> GetFuturesAsync(ISecurity security);
        IAsyncEnumerable<ISecurity> GetOptionsAsync(ISecurity security);
        Task<IDeal?> GetLastDealAsync(ISecurity security);
        IAsyncEnumerable<IDeal> GetDealsAsync(ISecurity security);
    }
}
