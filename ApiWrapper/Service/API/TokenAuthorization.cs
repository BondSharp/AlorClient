using System.Net.Http.Json;

namespace ApiWrapper
{
    internal class TokenAuthorization
    {
        private const string DevelopmentAddress = "https://oauthdev.alor.ru";
        private const string ProductionAddress = "https://oauth.alor.ru";

        private readonly string refreshToken;
        private readonly bool isProduction;
        private Task<string> token;
        public TokenAuthorization(Settings settings)
        {
            isProduction = settings.IsProduction;
            refreshToken = settings.RefreshToken;
            token = GetAssetTokenAsync();
        }

        public Task<string> Token()
        {
            return token;
        }

        private async Task<string> GetAssetTokenAsync()
        {
            var baseAddress = new Uri(isProduction ? ProductionAddress : DevelopmentAddress);

            using (var client = new HttpClient() { BaseAddress = baseAddress })
            {
                var response = await client.PostAsync($"/refresh?token={refreshToken}", null);
                var token = await response.Content.ReadFromJsonAsync<Token>();
                if (token == null)
                {
                    throw new NullReferenceException(nameof(token));
                }
                return token.AccessToken;
            }
        }
    }

}
