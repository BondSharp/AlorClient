using AlorClient.Domain;
using Common;

namespace AlorClient
{
    public interface ISecurities
    {
        Task<ISecurity> GetAsync(string symbol);
        IAsyncEnumerable<ISecurity> GetFuturesAsync(string symbol);
        IAsyncEnumerable<ISecurity> GetOptionsAsync(string symbol);
        Task<Deal?> GetLastDealAsync(ISecurity security);
        IAsyncEnumerable<Deal> GetDealsAsync(ISecurity security);
    }
}
