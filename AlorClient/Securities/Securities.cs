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

        public Task<ISecurity[]> GetFuturesAsync(string symbol)
        {

            return api.GetSecurities("FF", symbol);

        }

        public async Task<ISecurity> GetAsync(string symbol)
        {
            var share = await api.GetSecurity(symbol);
            return share;
        }

        public Task<ISecurity[]> GetOptionsAsync(string symbol)
        {
            return api.GetSecurities("O", symbol);
        }


    }
}
