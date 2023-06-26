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

        public async IAsyncEnumerable<ISecurity> GetOptionsAsync(string symbol)
        {
            await foreach (var option in api.GetSecurities("O", symbol))
            {

                yield return option;
            }
        }


    }
}
