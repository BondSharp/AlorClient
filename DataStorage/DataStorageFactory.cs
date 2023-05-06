

using Data;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

namespace DataStorage
{
    internal class DataStorageFactory : IDataStorageFactory
    {
        private readonly SecurityStorage securityStorage;
        private readonly IServiceProvider serviceProvider;

        public DataStorageFactory(SecurityStorage securityStorage, IServiceProvider serviceProvider)
        {
            this.securityStorage = securityStorage;
            this.serviceProvider = serviceProvider;
        }

        public IDataStorage<IDeal> DeadFactory(ISecurity security)
        {
            return GetDataStorage<DealStorage>(security);
        }

        private T GetDataStorage<T>(ISecurity aSecurity)
        {
            var security = securityStorage.Get(aSecurity);
            return ActivatorUtilities.CreateInstance<T>(serviceProvider, security);
        }


    }
}
