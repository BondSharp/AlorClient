namespace ApiWrapper
{
    public interface SecuritieIntarface
    {
        Share[] Shares(string? query = null);
        Future[] Futures(string? query = null);
        Option[] Options(string? query = null);
    }
}
