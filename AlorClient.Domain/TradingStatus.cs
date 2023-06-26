using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlorClient.Domain
{
    public enum TradingStatus
    {
        BreakTrading = 2,
        NormalTrading = 17,
        NotAvailableForTrading = 18,
        СlosingAuction = 102,
        ClosingPeriod = 103,
        DarkPooLAuction = 106,
        DiscreteAuction = 107,
        OpeningPeriod = 118,
        OpeningAuction = 119,
        ClosingAuctionPrice = 120
    }
}
