using Microsoft.AspNetCore.Http.Extensions;

namespace ApiWrapper
{
    internal class Securities : ISecurities
    {
        private const string path = "/md/v2/Securities";

        private readonly Dictionary<string, string> codesFutures;
        private readonly AlorApi alorApi;

        public Securities(AlorApi alorApi, Settings settings)
        {
            codesFutures = settings.CodesFutures;
            this.alorApi = alorApi;
        }

        public IAsyncEnumerable<Future> GetFuturesAsync(Security security)
        {
            if (codesFutures.TryGetValue(security.Symbol, out var code))
            {
                return GetSecurities<Future>("FF", code);
            }
            throw new Exception($"Not found key '{security.Symbol}' of 'СodesFutures'");
        }

        public IAsyncEnumerable<Option> GetOptionsAsync(Security security)
        {
            return GetSecurities<Option>("O", security.Symbol);
        }

        public async Task<Share> GetShareAsync(string symbol)
        {
            var share = await alorApi.Get<Share>($"{path}/MOEX/{symbol}", new QueryBuilder());
            return share;
        }

        public async IAsyncEnumerable<T> GetSecurities<T>(string cficode, string query)
        {
            var limit = 20;
            var offset = 0;
            var @params = new Dictionary<string, string>
            {
                { "cficode", cficode },
                { "limit", limit.ToString() },
                { "exchange", "MOEX" },
                { "query",  query}
            };

            while (true)
            {
                @params["offset"] = offset.ToString();
                var securities = await alorApi.Get<T[]>(path, new QueryBuilder(@params));
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
    }
}
