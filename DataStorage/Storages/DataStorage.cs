
using LiteDB;

namespace DataStorage
{
    internal abstract class DataStorage<Data,T> : IDataStorage<T>
    {
        private readonly ILiteDatabase liteDatabase;

        public DataStorage(ILiteDatabase liteDatabase)
        {
            this.liteDatabase = liteDatabase;          
        }

        public void Insert(T read)
        {
            var data = Map(read);
            GetCollection().Insert(data);
        }

        protected abstract Data Map(T read);

        private ILiteCollection<Data> GetCollection()
        {
            var collection = liteDatabase.GetCollection<Data>();

            return collection;
        }
    }
}
