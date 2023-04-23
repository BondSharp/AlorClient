using Data;

namespace AlorClient
{
    public abstract class Security : ISecurity
    {
        public required string Symbol { get; set; }

        public required string Shortname { get; set; }

        public required string Exchange { get; set; }

        public required string CfiCode { get; set; }

        public required string Board { get; set; }

        public required TradingStatus TradingStatus { get; set; }
    }
}
