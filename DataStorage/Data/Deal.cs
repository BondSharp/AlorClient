using Data;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    internal class Deal : BaseData, IDeal
    {
        [BsonId(true)]
        public long PrimeKey { get; set; }
        public long Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int OpenInterest { get; set; }
        public Side Side { get; set; }
    }
}
