namespace Common
{
    public class OptionsBoard
    {
        public OptionsBoardItem[] Items { get; }
        public DateTimeOffset ExpirationDate { get; }

        public OptionsBoard(OptionsBoardItem[] items, DateTimeOffset expirationDate)
        {
            Items = items;
            ExpirationDate = expirationDate;
        }
   
    }
}
