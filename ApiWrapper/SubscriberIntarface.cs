using ApiWrapper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper
{
    public interface SubscriberIntarface
    {
        IObservable<Message> Messages { get; }
        IObservable<Notification> Notifications { get; }

        void Subscribe(Subscription subscription);
        public void UnSubscribe(Subscription subscription);
    }
}
