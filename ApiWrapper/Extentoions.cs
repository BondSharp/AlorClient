using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ApiWrapper
{
    public static class Extentoions
    {
        const string ConfigurationKey = "ApiWrapper";

        public static IServiceCollection AddApiWrapper(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var settings = configuration.GetRequiredSection(ConfigurationKey).Get<Settings>()
                ?? throw new Exception($"Not found configuration wthi key '{ConfigurationKey}'");
            return serviceCollection
                    .AddSingleton(settings)
                    .AddSingleton<AlorApi>()
                    .AddSingleton<Securities>()
                    .AddSingleton<TokenAuthorization>();
        }
    }
}
