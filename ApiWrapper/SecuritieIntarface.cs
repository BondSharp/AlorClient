namespace ApiWrapper
{
    public interface SecuritieIntarface
    {
        IAsyncEnumerable<Share> Shares(string? query = null);
        IAsyncEnumerable<Future> Futures(string? query = null);
        IAsyncEnumerable<Option> Options(string? query = null);
    }
}
