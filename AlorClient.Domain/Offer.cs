using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlorClient.Domain
{
    public class Offer
    {
        public double Price { get; }
        public int Volume { get; }

        public required OrderBook OrderBook { get; set; }
    }
}
