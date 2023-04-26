using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Services;
using Microsoft.VisualBasic.FileIO;

namespace Data
{
    public class OptionsBoardFactory
    {
        private readonly Regex strikeRegex;
        private readonly SecurityCfi securityCfi;

        public OptionsBoardFactory(SecurityCfi securityCfi)
        {
            strikeRegex = new Regex(@"^\D+(\d+\.?\d+?).+$");
            this.securityCfi = securityCfi;
        }

        public OptionsBoard[] Factory(ISecurity[] options)
        {

            return new OptionsBoard[0];
        }

        //private OptionsBoard[] GetOptionsBoard(ISecurity[] options, DateTime expirationDate)
        //{
           
        //    return new OptionsBoard();
        //}


        //public async IAsyncEnumerable<OptionsBoard> GetOptionsBoardsAsync(Security security)
        //{
        //    var options = await GetOptionsAsync(security)
        //        .ToArrayAsync();
        //    foreach (var groupOptions in options.GroupBy(option => new { option.ExpirationDate, option.Strike }))
        //    {
        //        var call = groupOptions.FirstOrDefault(options => options.OptionType == OptionType.Сall);
        //        var put = groupOptions.FirstOrDefault(options => options.OptionType == OptionType.Put);
        //        if (put != null && call != null)
        //        {
        //            yield return new OptionsBoard(call, put, groupOptions.Key.Strike, groupOptions.Key.ExpirationDate);
        //        }
        //    }
        //}



        //private double GetStrike(Option option)
        //{
        //    var match = strikeRegex.Match(option.Shortname);
        //    if (match.Success)
        //    {
        //        var strike = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        //        return strike;
        //    }

        //    throw new ArgumentException(nameof(option));
        //}
    }
}
