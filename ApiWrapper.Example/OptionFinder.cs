using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper.Example
{
    public class OptionFinder
    {
        private readonly ISecurities securities;
        private readonly Security security;
        private readonly CancellationToken stoppingToken;
        private Task<Deal?> lastDeal;

        public OptionFinder(ISecurities securities, Security security, CancellationToken stoppingToken)
        {
            lastDeal = securities.GetLastDealAsync(security);
            this.securities = securities;
            this.security = security;
            this.stoppingToken = stoppingToken;
        }

        public async Task<Option?> FindOptionCall()
        {
            var deal = await lastDeal;
            if (deal != null)
            {
                return await GetOptions(OptionType.Сall)
                .Where(option => option.Strike >= deal.Price)
                .OrderBy(option => option.ExpirationDate)
                .ThenBy(option => option.Strike)
                .FirstOrDefaultAsync(stoppingToken);
            }

            return null;
        }

        public async Task<Option?> FindOptionPut()
        {
            var deal = await lastDeal;
            if (deal != null)
            {
                return await GetOptions(OptionType.Put)
                .Where(option => option.Strike >= deal.Price)
                .OrderBy(option => option.ExpirationDate)
                .ThenBy(option => option.Strike)
                .FirstOrDefaultAsync(stoppingToken);
            }

            return null;
        }


        private IAsyncEnumerable<Option> GetOptions(OptionType optionType)
        {
            return securities.GetOptionsAsync(security, optionType)
               .Where(x => x.ExpirationDate > DateTimeOffset.Now.Date);
        }
    }
}
