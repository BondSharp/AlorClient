using Data;

namespace AlorClient
{
    public class DealMessage : SecurityMessage
    {
        public DealsSubscription AllDealsSubscription { get; }
        public IDeal Deal { get; }

        internal DealMessage(DealsSubscription allDealsSubscription, Deal deal): base(allDealsSubscription)
        {
            AllDealsSubscription = allDealsSubscription;
            Deal = deal;
        }
    }
}
