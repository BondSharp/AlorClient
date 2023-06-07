using Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AlorClient
{
    internal class Securities : ISecurities
    {
        private readonly Dictionary<string, string> codesFutures;
        private readonly SecuritiesApi api;

        public Securities(SecuritiesApi securitiesApi, Settings settings)
        {
            codesFutures = settings.CodesFutures;
            api = securitiesApi;
        }

        private string GetCode(ISecurity security)
        {
            if (codesFutures.TryGetValue(security.Symbol, out var code))
            {
                return code;
            }

            throw new Exception($"Not found key '{security.Symbol}' of 'СodesFutures'");
        }

        public IAsyncEnumerable<ISecurity> GetFuturesAsync(ISecurity security)
        {
            var code = GetCode(security);

            return api.GetSecurities("FF", code);

        }

        public async Task<ISecurity> GetShareAsync(string symbol)
        {
            var share = await api.GetSecurity<Security>(symbol);
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

        public async IAsyncEnumerable<ISecurity> GetOptionsAsync(ISecurity security)
        {
            var code = GetCode(security);

            await foreach (var option in api.GetSecurities("OCE", code)) 
            {

                yield return option;
            }

            await foreach (var option in api.GetSecurities("OPE", code))
            {

                yield return option;
            }
        }


    }
}
