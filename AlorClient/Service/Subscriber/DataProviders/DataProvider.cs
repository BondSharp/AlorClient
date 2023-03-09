
using AlorClient.Service.WebSocket.DataProviders;

namespace AlorClient
{
    internal class DataProvider : IDataProvider
    {
        public IObservable<Message> Messages { get; }
        public IObservable<Notification> Notifications { get; }
        public IObservable<Reconnect> Reconnects { get; }

        public DataProvider(MessageProvider messageProvider, NotificationProvider notificationProvider, ReconnectProvider reconnectProvider)
        {
            Messages = messageProvider;
            Notifications = notificationProvider;
            Reconnects = reconnectProvider;
        }
    }
}
