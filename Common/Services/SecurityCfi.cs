using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class SecurityCfi
    {
        public bool IsOptionCall(ISecurity security)
        {
            return security.CfiCode.StartsWith("OC");
        }

        public bool IsOptionPut(ISecurity security)
        {
            return security.CfiCode.StartsWith("OP");
        }
    }
}
