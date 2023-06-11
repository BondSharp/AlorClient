using Common;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    internal class DealStorage : DataStorage<Deal,IDeal>, IDealStorage
    {
        private readonly Security security;

        public DealStorage(ILiteDatabase liteDatabase, Security security) : base(liteDatabase)
        {
            this.security = security;
        }

        protected override Deal Map(IDeal read)
        {
            return new Deal()
            {
                ForeignKey = security.PrimeKey,
                Id = read.Id,
                OpenInterest = read.OpenInterest,
                Price = read.Price,
                Quantity = read.Quantity,
                Side = read.Side,
                ClientTimestamp = read.ClientTimestamp,
                Timestamp = read.Timestamp,
            };
        }
    }
}
