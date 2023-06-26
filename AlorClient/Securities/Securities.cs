using System.Globalization;
using System.Text.RegularExpressions;
using AlorClient.Domain;

namespace AlorClient
{
    internal class Securities : ISecurities
    {
        private readonly SecuritiesApi api;

        public Securities(SecuritiesApi securitiesApi)
        {
            api = securitiesApi;
        }

        public async Task<Instrument[]> GetFuturesAsync(string symbol)
        {
            var result = await api.GetSecurities("FF", symbol);

            return result.Select(Map).ToArray();

        }

        public async Task<Instrument> GetAsync(string symbol)
        {
            var share = await api.GetSecurity(symbol);
            return Map(share);
        }

        public async Task<Instrument[]> GetOptionsAsync(string symbol)
        {
            var result = await api.GetSecurities("O", symbol);

            return result.Select(Map).ToArray();
        }


        private Instrument Map(SecurityDto securityDto)
        {
            return new Instrument()
            {
                Symbol = securityDto.Symbol,
                CfiCode = securityDto.CfiCode,
                Shortname = securityDto.Shortname,
                Cancellation = securityDto.Cancellation,
            };
        }


    }
}
