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
            var result = new List<OptionsBoard>();


            foreach (var group in options.GroupBy(option => option.Cancellation))
            {
                var items = GetItems(group).ToArray();

                result.Add(new OptionsBoard(items, group.Key));
            }

            return result.ToArray();
        }



        private IEnumerable<OptionsBoardItem> GetItems(IEnumerable<ISecurity> options)
        {

            foreach (var group in options.GroupBy(GetStrike))
            {
                var call = group.FirstOrDefault(securityCfi.IsOptionCall);
                var put = group.FirstOrDefault(securityCfi.IsOptionPut);
                if (put != null && call != null)
                {
                    yield return new OptionsBoardItem(call, put, group.Key);
                }
            }

        }

        private double GetStrike(ISecurity option)
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
