using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IOrderBook
    {
        IOffer[] Bids { get; }

        IOffer[] Asks { get; }

        DateTimeOffset Timestamp { get; }
    }
}
