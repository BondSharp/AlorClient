using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper
{
    internal class Token
    {
        public required string AccessToken { get; set; }
        public DateTimeOffset Created { get; private set; } = DateTimeOffset.Now;
    }
}
