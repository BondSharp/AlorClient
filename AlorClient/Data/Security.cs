using System.Text.Json.Serialization;
using Common;

namespace AlorClient
{
    internal  class Security : ISecurity
    {
        public required string Symbol { get; set; }

        public required string Shortname { get; set; }

        public required string Exchange { get; set; }

        public required string CfiCode { get; set; }

        public required string Board { get; set; }

        public DateTime Cancellation { get; set; }
    }
}
