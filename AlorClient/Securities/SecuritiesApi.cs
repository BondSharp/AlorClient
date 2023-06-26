using AlorClient.Domain;
using Common;
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

        public async Task<ISecurity> GetSecurity(string symbol)
        {
            var security = await alorApi.Get<Security>($"{basePath}/MOEX/{symbol}");

            return security;
        }

        public async IAsyncEnumerable<ISecurity> GetSecurities(string cficode, string query, int limit = 10000)
        {
            var offset = 0;

            while (true)
            {
                var queryBuilder = new QueryBuilder()
                    {
                        { "cficode", cficode },
                        { "query", query },
                        { "limit", limit.ToString() },
                        { "exchange", "MOEX" },
                        { "orderBy" , "cancellation"} ,
                        { "offset" , offset.ToString()} ,
                    };

                var securities = await alorApi.Get<Security[]>(basePath, queryBuilder);
                foreach (var security in securities)
                {
                    yield return security;
                }
                offset += securities.Length;
                if (securities.Length < limit)
                {
                    break;
                }
            }
        }

        public async IAsyncEnumerable<DealDto> GetDealsAsync(string symbol, bool descending, int batchSize)
        {
            var path = $"{basePath}/MOEX/{symbol}/alltrades";

            DateTimeOffset? from = null;
            while (true)
            {
                var queryBuilder = new QueryBuilder()
                {
                    { "descending", descending.ToString() },
                    { "includeVirtualTrades", "true" },
                    { "exchange", "MOEX" },
                    { "take" , batchSize.ToString()} ,
                };
                if (from.HasValue)
                {
                    queryBuilder.Add("from", from.Value.ToUnixTimeSeconds().ToString());
                }

                var deals = await alorApi.Get<DealDto[]>(path, queryBuilder);

                foreach (var deal in deals)
                {
                    yield return deal;
                }

                if (deals.Length == 0)
                {
                    break;
                }

                from = descending
                    ? deals.Min(deal => deal.Timestamp)
                    : deals.Max(deal => deal.Timestamp);

            }
        }
    }
}
