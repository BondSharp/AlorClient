using Common;
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
        public required long Id { get; set; }
        public required double Price { get; set; }
        public required int Quantity { get; set; }
        public required int OpenInterest { get; set; }
        public required Side Side { get; set; }
    }
}
