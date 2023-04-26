namespace Data
{
    public class OptionsBoard
    {
        public ISecurity Call { get; }
        public ISecurity Put { get; }
        public double Strike { get; }
        public DateTimeOffset ExpirationDate { get; }

        public OptionsBoard(ISecurity call, ISecurity put, double strike , DateTimeOffset expirationDate)
        {
            Call = call;
            Put = put;
            Strike = strike;
            ExpirationDate = expirationDate;
        }
   
    }
}
