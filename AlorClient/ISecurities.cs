using AlorClient.Domain;

namespace AlorClient
{
    public interface ISecurities
    {
        Task<Instrument> GetAsync(string symbol);
        Task<Instrument[]> GetFuturesAsync(string symbol);
        Task<Instrument[]> GetOptionsAsync(string symbol);
    }
}
