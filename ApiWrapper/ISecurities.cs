namespace ApiWrapper
{
    public interface ISecurities
    {
        Task<Share> GetShareAsync(string symbol);
        IAsyncEnumerable<Future> GetFuturesAsync(Security security);
        IAsyncEnumerable<Option> GetOptionsAsync(Security security, OptionType optionType);

        Task<Deal?> GetLastDealAsync(Security security);
    }
}
