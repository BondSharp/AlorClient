
using AlorClient.Domain;
using Common;

namespace AlorClient
{
    public class DealMessage : SecurityMessage
    {
        public DealsSubscription AllDealsSubscription { get; }
        public DealDto Deal { get; }

        internal DealMessage(DealsSubscription allDealsSubscription, DealDto deal): base(allDealsSubscription)
        {
            AllDealsSubscription = allDealsSubscription;
            Deal = deal;
        }
    }
}
