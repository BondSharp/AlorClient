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
        private readonly IDataStorageWriter writer;

        public SimpleDataWriter(ISubscriber subscriber, IDataStorageWriter writer)
        {
            this.subscriber = subscriber;
            this.writer = writer;
        }

        public IDisposable Write()
        {
            return subscriber.DataProvider.Messages.OfType<SecurityMessage>().Subscribe(OnMessage);
        }

        private void OnMessage(SecurityMessage message)
        {
            if (message is DealMessage dealMessage)
            {
                writer.Write(dealMessage.SecuritySubscription.Security, dealMessage.Deal);
            }
        }
    }
}
