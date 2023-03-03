namespace ApiWrapper
{
    public class OptionsBoard
    {
        public Option[] Calls { get; }
        public Option[] Puts { get; }
        public DateTimeOffset ExpirationDate { get; }

        public OptionsBoard(Option[] calls, Option[] puts, DateTimeOffset expirationDate)
        {
            Calls = calls;
            Puts = puts;
            ExpirationDate = expirationDate;
        }
   
    }
}
