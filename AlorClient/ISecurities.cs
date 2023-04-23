using Data;

namespace AlorClient
{
    public interface ISecurities
    {
        Task<Share> GetShareAsync(string symbol);
        IAsyncEnumerable<Future> GetFuturesAsync(Security security);
        IAsyncEnumerable<OptionsBoard> GetOptionsBoardsAsync(Security security);
        IAsyncEnumerable<Option> GetOptionsAsync(Security security);
        Task<IDeal?> GetLastDealAsync(Security security);
        IAsyncEnumerable<IDeal> GetDealsAsync(Security security);
    }
}
