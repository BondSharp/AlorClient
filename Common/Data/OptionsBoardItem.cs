using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class OptionsBoardItem
    {
        public ISecurity Call { get; }
        public ISecurity Put { get; }
        public double Strike { get; }

        public OptionsBoardItem(ISecurity call, ISecurity put, double strike)
        {
            Call = call;
            Put = put;
            Strike = strike;
        }
    }
}
