using System.Globalization;
using System.Text.RegularExpressions;
using AlorClient.Domain;
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

        public async Task<Deal?> GetLastDealAsync(ISecurity security)
        {
            return await api.GetDealsAsync(security.Symbol, true, 1).Select(Map).FirstOrDefaultAsync();

        }

        public IAsyncEnumerable<Deal> GetDealsAsync(ISecurity security)
        {
            return api.GetDealsAsync(security.Symbol, true, 100).Select(Map);
        }

        private Deal Map(DealDto dealDto)
        {
            return new Deal()
            {
                Side = dealDto.Side,

            };
        }

        public async IAsyncEnumerable<ISecurity> GetOptionsAsync(string symbol)
        {
            await foreach (var option in api.GetSecurities("O", symbol))
            {

                yield return option;
            }
        }


    }
}
