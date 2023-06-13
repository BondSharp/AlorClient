using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlorClient;
using DataStorage;

namespace Monitoring
{
    internal class Tracker
    {
        private readonly ISubscriber subscriber;
        private readonly IDataStorageWriter writer;

        public Tracker(ISubscriber subscriber, IDataStorageWriter writer)
        {
            this.subscriber = subscriber;
            this.writer = writer;
        }

        public IDisposable Tracke()
        {
            return subscriber.DataProvider.Messages.Subscribe(OnMessage);
        }

        private void OnMessage(Message message)
        {
            if (message is DealMessage dealMessage)
            {
                writer.Write(dealMessage.SecuritySubscription.Security, dealMessage.Deal);
            }
        }


    }
}
