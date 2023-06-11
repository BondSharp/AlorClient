using System.Globalization;
using System.Text.RegularExpressions;
using Common;

namespace AlorClient
{
    internal class Securities : ISecurities
    {
        private readonly SecuritiesApi api;

        public Securities(SecuritiesApi securitiesApi)
        {
            api = securitiesApi;
        }

        public IAsyncEnumerable<ISecurity> GetFuturesAsync(string symbol)
        {

            return api.GetSecurities("FF", symbol);

        }

        public async Task<ISecurity> GetAsync(string symbol)
        {
            var share = await api.GetSecurity(symbol);
            return share;
        }

        public async Task<IDeal?> GetLastDealAsync(ISecurity security)
        {
            return await api.GetDealsAsync(security.Symbol, true, 1).FirstOrDefaultAsync();

        }

        public IAsyncEnumerable<IDeal> GetDealsAsync(ISecurity security)
        {
            return api.GetDealsAsync(security.Symbol, true, 100);
        }

        public async IAsyncEnumerable<ISecurity> GetOptionsAsync(string symbol)
        {

            await foreach (var option in api.GetSecurities("OCE", symbol)) 
            {

                yield return option;
            }

            await foreach (var option in api.GetSecurities("OPE", symbol))
            {

                yield return option;
            }
        }


    }
}
