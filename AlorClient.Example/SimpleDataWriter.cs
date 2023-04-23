using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlorClient.Example
{
    internal class SimpleDataWriter
    {
        private readonly ISubscriber subscriber;
        private readonly IDataStorageFactory dataStorageFactory;

        public SimpleDataWriter(ISubscriber subscriber, IDataStorageFactory dataStorageFactory)
        {
            this.subscriber = subscriber;
            this.dataStorageFactory = dataStorageFactory;
        }

        public IDisposable Write()
        {
            return subscriber.DataProvider.Messages.OfType<SecurityMessage>().Subscribe(OnMessage);
        }

        private void OnMessage(SecurityMessage message)
        {
            if (message is DealMessage dealMessage)
            {
                dataStorageFactory
                    .DeadFactory(dealMessage.SecuritySubscription.Security)
                    .Insert(dealMessage.Deal);
            }
        }
    }
}
