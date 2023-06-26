using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlorClient.Domain
{
    public class Share : IInstrumentProperty
    {
        public required Instrument Instrument { get; set; }
    }
}
