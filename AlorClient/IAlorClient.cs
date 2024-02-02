using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlorClient;
public interface IAlorClient
{
    IAsyncEnumerable<Security> GetSecurities(TimeSpan duration);
    IAsyncEnumerable<Deal> GetAllDeals(Security security, int batch, Deal? lastDeal);
    IAsyncEnumerable<Deal> GetHistoryDeals(Security security, int batch, DateTimeOffset? dateTime);
}
