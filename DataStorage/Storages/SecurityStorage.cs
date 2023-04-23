using Data;
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

        public Security Get(ISecurity shortSecurity)
        {
            return Find(shortSecurity)
                ?? Insert(new Security() { Symbol = shortSecurity.Symbol });
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
