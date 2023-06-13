using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.DependencyInjection;

namespace DataStorage
{
    internal class DataStorageWriter : IDataStorageWriter
    {

        private readonly SecurityStorage securityStorage;
        private readonly IServiceProvider serviceProvider;

        public DataStorageWriter(SecurityStorage securityStorage, IServiceProvider serviceProvider)
        {
            this.securityStorage = securityStorage;
            this.serviceProvider = serviceProvider;
        }

        public void Write(ISecurity security, IDeal deal)
        {
            DeadFactory(security).Insert(deal);
        }

        public void Write(ISecurity security, IOrderBook orderBook)
        {

        }

        private DealStorage DeadFactory(ISecurity security)
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
