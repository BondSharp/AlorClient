
using AlorClient.Service.WebSocket.DataProviders;

namespace AlorClient;

internal class DataProvider : IDataProvider
{
    public IObservable<Message> Messages { get; }
    public IObservable<Reconnect> Reconnects { get; }

    public DataProvider(MessageProvider messageProvider, ReconnectProvider reconnectProvider)
    {
        Messages = messageProvider;
        Reconnects = reconnectProvider;
    }
}
