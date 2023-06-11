using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public static class Extentoions
    {
        public static IServiceCollection AddData(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<SecurityCfi>();
            serviceCollection.AddSingleton<OptionsBoardFactory>();

            return serviceCollection;
        }
    }
}
