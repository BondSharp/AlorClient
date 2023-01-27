using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper
{
    public class Securities
    {
        private const string path = "/md/v2/Securities";
        private readonly AlorApi alorApi;

        public Securities(AlorApi alorApi)
        {
            this.alorApi = alorApi;
        }

        public Security[] Get()
        {
            var result = alorApi.Get<Security[]>(path, new Dictionary<string, string>());

            return result.Result;
        }
    }
}
