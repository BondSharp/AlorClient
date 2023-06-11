using Common;

namespace AlorClient
{
    public interface ISecurities
    {
        Task<ISecurity> GetAsync(string symbol);
        IAsyncEnumerable<ISecurity> GetFuturesAsync(string symbol);
        IAsyncEnumerable<ISecurity> GetOptionsAsync(string symbol);
        Task<IDeal?> GetLastDealAsync(ISecurity security);
        IAsyncEnumerable<IDeal> GetDealsAsync(ISecurity security);
    }
}
