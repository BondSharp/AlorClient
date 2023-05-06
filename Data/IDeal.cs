using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IDeal : ITimestamp
    {
        long Id { get; }
        double Price { get; }
        int Quantity { get; }
        int OpenInterest { get; }
        Side Side { get; }
    }
}
