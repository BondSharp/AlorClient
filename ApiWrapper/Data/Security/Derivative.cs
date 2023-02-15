using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiWrapper
{
    public abstract class Derivative : Security
    {
        [JsonPropertyName("cancellation")]
        public DateTime ExpirationDate { get; set; }
    }
}
