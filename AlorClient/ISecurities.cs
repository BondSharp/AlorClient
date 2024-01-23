using AlorClient.Data;
using AlorClient.Domain;

namespace AlorClient;

public interface ISecurities
{
    Task<Security[]> GetSharesAsync();

    Task<Security[]> GetFuturesAsync();

    Task<Option[]> GetOptionsAsync(Security security);
}
