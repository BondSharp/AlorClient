using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace ApiWrapper
{
    internal class AlorApi
    {
        private readonly TokenAuthorization tokenAuthorization;
        private readonly bool isProduction;
        private const string DevelopmentAddress = "https://apidev.alor.ru";
        private const string ProductionAddress = "https://api.alor.ru";

        public AlorApi(TokenAuthorization tokenAuthorization, Settings settings)
        {
            this.tokenAuthorization = tokenAuthorization;
            isProduction = settings.IsProduction;
        }

        public async Task<T> Get<T>(string path, Dictionary<string, string>? query = null) where T : class
        {


            using var client = CreateClient();

            var uri = GetUri(path, query);
            var resut = await client.GetFromJsonAsync<T>(uri);
            if (resut == null)
            {
                throw new NullReferenceException(nameof(resut));
            }
            return resut;
        }

        private Uri GetUri(string path, Dictionary<string, string>? query = null)
        {
            var queryBuilder = query != null ? new QueryBuilder(query) : null;

            using var client = CreateClient();

            var uriBuilder = new UriBuilder(isProduction ? ProductionAddress : DevelopmentAddress)
            {
                Path = path,
                Query = queryBuilder?.ToString()
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
}
