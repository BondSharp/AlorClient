using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApiWrapper
{
    public class AlorApi
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

        public async Task<T> Get<T>(string path, Dictionary<string, string> @params)
        {
            using (var client = new HttpClient())
            {
                var queryBuilder = new QueryBuilder(@params);
                
                var uriBuilder = new UriBuilder(isProduction ? ProductionAddress : DevelopmentAddress)
                {
                    Path = path,
                    Query = queryBuilder.ToString()

                };
             
                var resut = await client.GetFromJsonAsync<T>(uriBuilder.Uri);
                return resut;

            }
        }
    }
}
