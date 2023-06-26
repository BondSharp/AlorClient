using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlorClient.Domain
{
    public interface ITimestamp
    {
        DateTimeOffset Timestamp { get; }

        DateTimeOffset ClientTimestamp { get; set; }
    }
}
