namespace ApiWrapper
{
    public interface ISecurities
    {
        Task<Share> GetShareAsync(string symbol);
        IAsyncEnumerable<Future> GetFuturesAsync(Security share);
        IAsyncEnumerable<Option> GetOptionsAsync(Security share);
    }
}
