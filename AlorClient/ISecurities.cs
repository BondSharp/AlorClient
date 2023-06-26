using AlorClient.Domain;
using Common;

namespace AlorClient
{
    public interface ISecurities
    {
        Task<ISecurity> GetAsync(string symbol);
        Task<ISecurity[]> GetFuturesAsync(string symbol);
        Task<ISecurity[]> GetOptionsAsync(string symbol);
    }
}
