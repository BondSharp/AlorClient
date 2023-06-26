using AlorClient.Domain;
using Microsoft.AspNetCore.Http.Extensions;

namespace AlorClient
{
    internal class SecuritiesApi
    {
        private const string basePath = "/md/v2/Securities";
        private readonly AlorApi alorApi;

        public SecuritiesApi(AlorApi alorApi)
        {
            this.alorApi = alorApi;
        }

        public async Task<SecurityDto> GetSecurity(string symbol)
        {
            var security = await alorApi.Get<SecurityDto>($"{basePath}/MOEX/{symbol}");

            return security;
        }

        public async Task<SecurityDto[]> GetSecurities(string cficode, string query)
        {

            var queryBuilder = new QueryBuilder()
                    {
                        { "cficode", cficode },
                        { "query", query },
                        { "limit", "1000" },
                        { "exchange", "MOEX" },
                        { "orderBy" , "cancellation"} ,
                        { "offset" , "0"} ,
                    };

            var securities = await alorApi.Get<SecurityDto[]>(basePath, queryBuilder);

            return securities;

        }

    }
}
