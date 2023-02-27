using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper.Example
{
    public static class Expansions
    {
        public static OptionFinder CreateOptionFinder(this IServiceProvider serviceProvider, Security security, CancellationToken stoppingToken)
        {
            var optionFinder = ActivatorUtilities.CreateInstance<OptionFinder>(serviceProvider, security, stoppingToken);

            return optionFinder;
        }

    }
}
