using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper
{
    public sealed class Settings
    {
        public required string RefreshToken { get; set; }
        public required bool IsProduction { get; set; }
        public TimeSpan? ReconnectTimeout { get; set; }
        public TimeSpan? ErrorReconnectTimeout { get; internal set; }
    }
}
