namespace AlorClient
{
    public class OptionsBoard
    {
        public Option Call { get; }
        public Option Put { get; }
        public double Strike { get; }
        public DateTimeOffset ExpirationDate { get; }

        public OptionsBoard(Option call, Option put, double strike , DateTimeOffset expirationDate)
        {
            Call = call;
            Put = put;
            Strike = strike;
            ExpirationDate = expirationDate;
        }
   
    }
}
