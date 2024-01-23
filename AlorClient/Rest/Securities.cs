

using AlorClient.Data;
using Microsoft.AspNetCore.Http.Extensions;

namespace AlorClient;
internal class Securities : ISecurities
{
    private readonly AlorApi alorApi;

    public Securities(AlorApi alorApi)
    {
        this.alorApi = alorApi;
    }

    public async Task<Security[]> GetFuturesAsync()
    {
        return await GetSecurities<Security>(new[] { "FF" });
    }

    public async Task<Security[]> GetSharesAsync()
    {
        return await GetSecurities<Security>(new[] { "ES", "EP" });
    }

    public async Task<Option[]> GetOptionsAsync(Security security)
    {
        var result  = await GetSecurities<Option>(new []{ "O" },security.Symbol);

        return result.Where(x => x.UnderlyingSymbol == security.Symbol || x.UnderlyingSymbol == security.ShortName).ToArray();
    }


    private async Task<T[]> GetSecurities<T>(string[] cficodes, string? q = null) where T : Security
    {
        var result = new List<T>();
        foreach (var cficode in cficodes)
        {
            var queryBuilder = new QueryBuilder(new Dictionary<string, string>()
            {
                ["exchange"] = "MOEX",
                ["offset"] = "0",
                ["limit"] = "1000",
                ["cficode"] = cficode
            });

            if (!string.IsNullOrEmpty(q))
            {
                queryBuilder.Add("query", q);
            }
            var securities = await alorApi.Get<T[]>("/md/v2/Securities", queryBuilder);
            result.AddRange(securities);
        }

        return result.ToArray();
        
      
    }
}
