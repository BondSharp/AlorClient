using System.Text.Json.Serialization;
using Data;

namespace AlorClient
{
    internal abstract class Security : ISecurity
    {
        public required string Symbol { get; set; }

        public required string Shortname { get; set; }

        public required string Exchange { get; set; }

        public required string CfiCode { get; set; }

        public required string Board { get; set; }

        public DateTime Cancellation { get; set; }
    }
}
