
using AlorClient.Domain;

namespace AlorClient;

public interface ISecurities
{
    IAsyncEnumerable<Security> GetSecurities(TimeSpan duration);
}
