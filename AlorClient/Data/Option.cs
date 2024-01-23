using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlorClient.Data;
public class Option : Security
{
    public double StrikePrice { get; set; }
    public string UnderlyingSymbol { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OptionSide OptionSide { get; set; }

    public double TheorPrice { get; set; }
    public double TheorPriceLimit { get; set; }
    public double Volatility { get; set; }
}
