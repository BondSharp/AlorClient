using Common;
using LiteDB;

namespace DataStorage
{
    internal class SecurityStorage 
    {
        private readonly ILiteDatabase liteDatabase;

        public SecurityStorage(ILiteDatabase liteDatabase)
        {
            this.liteDatabase = liteDatabase;
        }

        public Security? Find(ISecurity shortSecurity)
        {
            if (shortSecurity is Security security)
            {
                return security;
            }
            return GetCollection().Query()
                .Where(x => x.Symbol == shortSecurity.Symbol)
                .FirstOrDefault();

        }

        public Security Get(ISecurity security)
        {
            return Find(security)
                ?? Insert(Map(security));
        }

        private Security Map(ISecurity security)
        {
            return new Security
            {
                Cancellation = security.Cancellation,
                CfiCode = security.CfiCode,
                Exchange = security.Exchange,
                Shortname = security.Shortname,
                Symbol = security.Symbol,
            };
        }

        public Security Insert(Security security)
        {
            GetCollection().Insert(security);

            return security;
        }

        ILiteCollection<Security> GetCollection()
        {
            var collections = liteDatabase
                 .GetCollection<Security>();
            collections
                  .EnsureIndex(x => new { x.Symbol });

            return collections;

        }
    }
}
