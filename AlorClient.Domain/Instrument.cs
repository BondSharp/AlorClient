using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlorClient.Domain
{
    public class Instrument
    {
        public long Id { get; internal set; }

        public required string Symbol { get; set; }

        public required string Shortname { get; set; }
        public required string CfiCode { get; set; }

    }
}
