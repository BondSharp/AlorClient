using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiWrapper.App
{
    public static class Extentoions
    {
        const string ConfigurationKey = "ApiWrapper";
     
        public static IServiceCollection AddApiWrapper(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var settings = configuration.GetRequiredSection(ConfigurationKey).Get<Settings>();
            return serviceCollection
                .AddSingleton<TokenAuthorization>((provider) =>
                {
                    return new TokenAuthorization(settings.IsProduction, settings.RefreshToken);
                });
        }
    }
}
