using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AlorClient;

internal class AlorApi
{
    private readonly TokenAuthorization tokenAuthorization;
    private readonly bool isProduction;

    private const string developmentAddress = "https://apidev.alor.ru";
    private const string productionAddress = "https://api.alor.ru";

    public AlorApi(TokenAuthorization tokenAuthorization, Settings settings)
    {
        this.tokenAuthorization = tokenAuthorization;
        isProduction = settings.IsProduction;
    }

    public async Task<T> Get<T>(string path) where T : class
    {
        return await Get<T>(path, new QueryBuilder());
    }
    public async Task<T> Get<T>(string path, QueryBuilder query) where T : class
    {
        using var client = CreateClient();

        var uri = GetUri(path, query);
        Console.WriteLine(uri);
        var result = await client.GetFromJsonAsync<T>(uri);

        return result!;
    }

    public async Task Download(string path, string file, QueryBuilder query)
    {
        using var client = CreateClient();

        var uri = GetUri(path, query);
        using var result = await client.GetStreamAsync(uri);
        File.Delete(file);
        using var fileStream = new FileStream(file, FileMode.Create, FileAccess.Write);
        await result.CopyToAsync(fileStream);

    }

    public async IAsyncEnumerable<T> Pagination<T>(string path,int  offset, int batch, QueryBuilder query, Func<T,int> count) where T : class
    {
        
        while (true)
        {
            var queryBuilder = new QueryBuilder(query);
            queryBuilder.Add("offset",offset.ToString());
            queryBuilder.Add("limit", batch.ToString());
            var result = await Get<T>(path,queryBuilder);
            yield return result;
            if (count(result) < batch)
            {
                break;
            }
            offset += batch;            
        }
    }

    private Uri GetUri(string path, QueryBuilder queryBuilder)
    {
        using var client = CreateClient();

        var uriBuilder = new UriBuilder(isProduction ? productionAddress : developmentAddress)
        {
            Path = path,
            Query = queryBuilder.ToString()
        };

        return uriBuilder.Uri;
    }

    private HttpClient CreateClient()
    {
        var client = new HttpClient();
        var token = tokenAuthorization.Token();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

        return client;
    }
}
