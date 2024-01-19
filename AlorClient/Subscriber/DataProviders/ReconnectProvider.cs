using System.Reactive.Linq;
using Websocket.Client;

namespace AlorClient;

internal class ReconnectProvider : IObservable<Reconnect>
{
    private readonly IWebsocketClient client;

    public ReconnectProvider(IWebsocketClient client)
    {
        this.client = client;
    }

    public IDisposable Subscribe(IObserver<Reconnect> observer)
    {
        return client
            .ReconnectionHappened
            .Where(reconnectionInfo => reconnectionInfo.Type != ReconnectionType.Initial)
            .Select(_ => new Reconnect())
            .Subscribe(observer);
    }
}
