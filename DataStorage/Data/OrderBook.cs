using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataStorage
{
    internal class OrderBook : BaseData, IOrderBook
    {
        public required Offer[] Bids { get; set; }

        public required Offer[] Asks { get; set; }

        IOffer[] IOrderBook.Bids => Bids;

        IOffer[] IOrderBook.Asks => Asks;
    }
}
