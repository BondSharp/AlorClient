using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
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

        public async Task<T> Get<T>(string path, QueryBuilder queryBuilder) where T : class
        {
            using var client = CreateClient();
       
            var uriBuilder = new UriBuilder(isProduction ? ProductionAddress : DevelopmentAddress)
            {
                Path = path,
                Query = queryBuilder.ToString()
            };

            var resut = await client.GetFromJsonAsync<T>(uriBuilder.Uri);
            if (resut == null)
            {
                throw new NullReferenceException(nameof(resut));
            }
            return resut;
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
