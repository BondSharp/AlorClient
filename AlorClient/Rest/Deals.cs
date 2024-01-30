using Microsoft.AspNetCore.Http.Extensions;

namespace AlorClient;
internal class Deals : IDeals
{
    private readonly AlorApi alorApi;

    public Deals(AlorApi alorApi)
    {
        this.alorApi = alorApi;
    }

    public async IAsyncEnumerable<Deal> GetAllDeal(Security security, int batch, Deal? @continue)
    {
        var offset = 0;
        var @params = new Dictionary<string, string>(3)
        {
            ["take"] = batch.ToString()
        };
        if (@continue != null)
        {
            @params.Add("fromId", @continue.Id.ToString());
            offset = 1;
        }

        while (true)
        {                     
            @params.Add("offset", offset.ToString());         

            var deals = await alorApi.Get<Deal[]>($"/md/v2/Securities/{security.Exchange}/{security.Symbol}/alltrades", new QueryBuilder(@params));

            foreach (var deal in deals)
            {
                yield return deal;
            }

            if (deals.Length != batch)
            {
                break;
            }
            offset += batch;
        };

    }
}
