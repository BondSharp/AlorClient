namespace AlorClient
{
    public interface ISecurities
    {
        Task<Share> GetShareAsync(string symbol);
        IAsyncEnumerable<Future> GetFuturesAsync(Security security);
        IAsyncEnumerable<OptionsBoard> GetOptionsBoardsAsync(Security security);
        IAsyncEnumerable<Option> GetOptionsAsync(Security security);
        Task<Deal?> GetLastDealAsync(Security security);
        IAsyncEnumerable<Deal> GetDealsAsync(Security security);
    }
}
