using System.Globalization;
using System.Text.RegularExpressions;

namespace ApiWrapper
{
    internal class Securities : ISecurities
    {
        private readonly Dictionary<string, string> codesFutures;
        private readonly SecuritiesApi api;
        private readonly Regex strikeRegex;

        public Securities(SecuritiesApi securitiesApi, Settings settings)
        {
            codesFutures = settings.CodesFutures;
            api = securitiesApi;
            strikeRegex = new Regex(@"^\D+(\d+\.?\d+?).+$");
        }

        public IAsyncEnumerable<Future> GetFuturesAsync(Security security)
        {
            if (codesFutures.TryGetValue(security.Symbol, out var code))
            {
                return api.GetSecurities<Future>("FF", code);
            }
            throw new Exception($"Not found key '{security.Symbol}' of 'СodesFutures'");
        }

        public async Task<Share> GetShareAsync(string symbol)
        {
            var share = await api.GetSecurity<Share>(symbol);
            return share;
        }

        public async Task<Deal?> GetLastDealAsync(Security security)
        {
            return await api.GetDealsAsync(security.Symbol, true, 1).FirstOrDefaultAsync();

        }

        public IAsyncEnumerable<Deal> GetDealsAsync(Security security)
        {
            return api.GetDealsAsync(security.Symbol, true, 100);
        }


        public async IAsyncEnumerable<OptionsBoard> GetOptionsBoardsAsync(Security security)
        {
            var options = await GetOptionsAsync(security).ToArrayAsync();
            foreach (var groupOptions in options.GroupBy(option => option.ExpirationDate).OrderBy(group => group.Key))
            {             
                var calls = GetOptions(groupOptions, OptionType.Сall);
                var puts = GetOptions(groupOptions, OptionType.Put);
                yield return new OptionsBoard(calls,puts,groupOptions.Key);
            }
            throw new NotImplementedException();
        }

        private Option[] GetOptions(IEnumerable<Option> options, OptionType optionType)
        {
            return options
                     .Where(option => option.OptionType == optionType)
                     .OrderBy(option => option.Strike)
                     .ToArray();
        }

        private async IAsyncEnumerable<Option> GetOptionsAsync(Security security)
        {
            await foreach (var option in api.GetSecurities<Option>("O", security.Symbol))
            {
                option.OptionType = GetOptionType(option);
                option.Strike = GetStrike(option);
                yield return option;
            }
        }

        private OptionType GetOptionType(Option option)
        {
            switch (option.CfiCode[1])
            {
                case 'C':
                    return OptionType.Сall;
                case 'P':
                    return OptionType.Put;
                default:
                    throw new ArgumentException(nameof(option));
            }
        }

        private double GetStrike(Option option)
        {
            var match = strikeRegex.Match(option.Shortname);
            if (match.Success)
            {
                var strike = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                return strike;
            }

            throw new ArgumentException(nameof(option));
        }
    }
}
