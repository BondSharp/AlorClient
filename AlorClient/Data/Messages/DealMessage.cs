namespace AlorClient
{
    public class DealMessage : SecurityMessage
    {
        public DealsSubscription AllDealsSubscription { get; }
        public Deal Deal { get; }

        public DealMessage(DealsSubscription allDealsSubscription, Deal deal): base(allDealsSubscription)
        {
            AllDealsSubscription = allDealsSubscription;
            Deal = deal;
        }
    }
}
