using Data;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    internal class DealStorage : DataStorage<Deal,IDeal>
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
                Security = security,
                Id = read.Id,
                OpenInterest = read.OpenInterest,
                Price = read.Price,
                Quantity = read.Quantity,
                Side = read.Side
            };
        }
    }
}
