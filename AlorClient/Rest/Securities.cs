

using System.IO;
using System.Text.Json;
using AlorClient.Data;
using Microsoft.AspNetCore.Http.Extensions;

namespace AlorClient;
internal class Securities : ISecurities
{
    private readonly AlorApi alorApi;

    private const string fileCache = "Securities.json";

    private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

    public Securities(AlorApi alorApi)
    {
        this.alorApi = alorApi;
    }

    private async Task Download(TimeSpan duration)
    {
        if (File.Exists(fileCache) && File.GetLastWriteTimeUtc(fileCache).Add(duration) > DateTime.UtcNow)
        {
            return;
        }

        var queryBuilder = new QueryBuilder(new Dictionary<string, string>()
        {
            ["offset"] = "0",
            ["limit"] = "9999999",
        });

        await alorApi.Download("md/v2/Securities/MOEX", fileCache, queryBuilder);
    }

    public async IAsyncEnumerable<Security> GetSecurities(TimeSpan duration)
    {
        await Download(duration);
        using var stream = File.Open(fileCache,FileMode.Open,FileAccess.Read);
        await foreach (var json in JsonSerializer.DeserializeAsyncEnumerable<JsonDocument>(stream))
        {
            yield return Parser(json!);
        };

        yield break;

    }

    private Security Parser(JsonDocument jsonDocument)
    {
        var cficode = jsonDocument.RootElement.GetProperty("cfiCode").GetString()!;
        if (cficode.StartsWith("OP") || cficode.StartsWith("OP"))
        {
            return Parser<Option>(jsonDocument);
        }

        if (cficode.StartsWith("FF"))
        {
            return Parser<Future>(jsonDocument);
        }

        if (cficode.StartsWith("ES") || cficode.StartsWith("EP"))
        {
            return Parser<Share>(jsonDocument);
        }

        if (cficode.StartsWith("MRC"))
        {
            return Parser<Currency>(jsonDocument);
        }
        return Parser<Security>(jsonDocument); 
    }

    private T Parser<T>(JsonDocument jsonDocument)
    {
        return jsonDocument.Deserialize<T>(jsonSerializerOptions)!;
    }
}
