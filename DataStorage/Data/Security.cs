using Data;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    internal class Security : ISecurity
    {
        [BsonId(true)]
        public long PrimeKey { get; set; }
        public required string Symbol { get; set; }
    }
}
