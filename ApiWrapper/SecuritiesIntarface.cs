namespace ApiWrapper
{
    public interface SecuritiesIntarface
    {
        IAsyncEnumerable<T> Get<T>(string? query = null) where T : Security;
    }
}
