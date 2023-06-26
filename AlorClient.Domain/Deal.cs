namespace AlorClient.Domain
{
    public class Deal
    {
        public long Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int OpenInterest { get; set; }
        public Side Side { get; set; }
    }
}