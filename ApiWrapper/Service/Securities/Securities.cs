using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ApiWrapper
{
    internal class Securities : ISecurities
    {
        private const string basePath = "/md/v2/Securities";

        private readonly Dictionary<string, string> codesFutures;
        private readonly AlorApi alorApi;
        private readonly Regex strikeRegex;

        public Securities(AlorApi alorApi, Settings settings)
        {
            codesFutures = settings.CodesFutures;
            this.alorApi = alorApi;

            strikeRegex = new Regex(@"^\D+(\d+\.?\d+).+$");
        }

        public IAsyncEnumerable<Future> GetFuturesAsync(Security security)
        {
            if (codesFutures.TryGetValue(security.Symbol, out var code))
            {
                return GetSecurities<Future>("FF", code);
            }
            throw new Exception($"Not found key '{security.Symbol}' of 'СodesFutures'");
        }

        public async IAsyncEnumerable<Option> GetOptionsAsync(Security security, OptionType optionType)
        {
            var cficode = string.Format("O{0}", optionType == OptionType.Put ? "P" : "C");
            await foreach (var option in GetSecurities<Option>(cficode, security.Symbol))
            {
                option.OptionType = GetOptionType(option);
                option.Strike = GetStrike(option);
                yield return option;
            }
        }

        public async Task<Share> GetShareAsync(string symbol)
        {
            var share = await alorApi.Get<Share>($"{basePath}/MOEX/{symbol}");
            return share;
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
            if (match.Success && double.TryParse(match.Groups[1].Value, out var strike))
            {
                return strike;
            }

            throw new ArgumentException(nameof(option));
        }

        private async IAsyncEnumerable<T> GetSecurities<T>(string cficode, string query)
        {
            var limit = 20;
            var offset = 0;
            var @params = new Dictionary<string, string>
            {
                { "cficode", cficode },
                { "limit", limit.ToString() },
                { "exchange", "MOEX" },
                { "query",  query}
            };

            while (true)
            {
                @params["offset"] = offset.ToString();
                var securities = await alorApi.Get<T[]>(basePath, @params);
                foreach (var security in securities)
                {
                    yield return security;
                }
                offset += securities.Length;
                if (securities.Length < limit)
                {
                    break;
                }
            }
        }

        public async Task<Deal?> GetLastDealAsync(Security security)
        {
            var path = $"{basePath}/{security.Exchange}/{security.Symbol}/alltrades";

            var @params = new Dictionary<string, string>
            {
                { "descending", true.ToString()},
                { "includeVirtualTrades", true.ToString() },
                { "take", 1.ToString() },
            };

            var deals = await alorApi.Get<Deal[]>(path, @params);

            return deals.FirstOrDefault();

        }
    }
}
