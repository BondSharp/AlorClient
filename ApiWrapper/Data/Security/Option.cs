using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiWrapper
{
    public class Option : Security
    {

        [JsonIgnore]
        public double Strike { get; set; }

    }
}
