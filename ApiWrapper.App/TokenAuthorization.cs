using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace ApiWrapper.App
{
    public class TokenAuthorization
    {
        private const string DevelopmentAddress = "https://oauthdev.alor.ru";
        private const string ProductionAddress = "https://oauth.alor.ru";
        private readonly string refreshToken;
        private readonly HttpClient client;

        public TokenAuthorization(bool isProduction, string refreshToken)
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri(isProduction ? ProductionAddress : DevelopmentAddress ),
            };
            this.refreshToken = refreshToken;
        }

        public Task<string> Token()
        {
            return GetAssetTokenAsync();
        }

        private async Task<string> GetAssetTokenAsync()
        {
            var response = await client.PostAsync($"/refresh?token={refreshToken}",null);

            var token = await response.Content.ReadFromJsonAsync<Token>();

            return token.AccessToken;
        }
    }

}
