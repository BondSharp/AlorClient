using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring
{
    internal class Configuration
    {
        public required string[] Symbols { get; set; }

        public required string[] Futures { get; set; } = new string[0];

        public required string[] Options { get; set; } = new string[0];

    }
}
