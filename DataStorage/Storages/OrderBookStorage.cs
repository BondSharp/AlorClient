

using Common;
using LiteDB;

namespace DataStorage
{
    internal class OrderBookStorage : DataStorage<OrderBook, IOrderBook>
    {

        private readonly Security security;

        public OrderBookStorage(ILiteDatabase liteDatabase, Security security) : base(liteDatabase)
        {
            this.security = security;
        }
        protected override OrderBook Map(IOrderBook read)
        {
            var asks = read.Asks.Select(Map).ToArray();
            var bids = read.Bids.Select(Map).ToArray();

            return new OrderBook()
            {
                Asks = asks,
                Bids = bids,
                ClientTimestamp = read.ClientTimestamp,
                Timestamp = read.Timestamp,
                ForeignKey = security.PrimeKey
            };
        }

        private Offer Map(IOffer offer)
        {
            return new Offer()
            {
                Price = offer.Price,
                Volume = offer.Volume,
            };
        }
    }
}
