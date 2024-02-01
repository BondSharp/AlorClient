using Microsoft.AspNetCore.Http.Extensions;

namespace AlorClient;
internal class Deals : IDeals
{
    private readonly AlorApi alorApi;

    public Deals(AlorApi alorApi)
    {
        this.alorApi = alorApi;
    }

    public IAsyncEnumerable<Deal> GetAllDeals(Security security, int batch, Deal? @continue)
    {
        var path = $"/md/v2/Securities/{security.Exchange}/{security.Symbol}/alltrades";

        var offset = 0;
        var @params = new QueryBuilder();
        if (@continue != null)
        {
            @params.Add("fromId", @continue.Id.ToString());
            offset = 1;
        }

        return alorApi.Pagination<Deal[]>(path, offset, batch, @params,x=>x.Length)                
                .SelectMany(x => x.ToAsyncEnumerable());                
    }

    public  IAsyncEnumerable<Deal> GetHistoryDeals(Security security, int batch, DateTimeOffset? dateTime)
    {
        var path = $"/md/v2/Securities/{security.Exchange}/{security.Symbol}/alltrades/history";
        var query = new QueryBuilder();
        if (dateTime.HasValue)
        {
            query.Add("from", dateTime.Value.ToUnixTimeSeconds().ToString());
        }

        return alorApi
            .Pagination<ListDeal>(path, 0, batch, query, list => list.List.Length)
            .SelectMany(x=>x.List.ToAsyncEnumerable());
    }
}
