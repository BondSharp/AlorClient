namespace AlorClient.Domain
{
    public interface ISecurity
    {
        string Symbol { get; }
        string Exchange { get; }
        string Shortname { get; }
        string CfiCode { get; }
        DateTime Cancellation { get; }
    }
}
