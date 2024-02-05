using Websocket.Client;
using Websocket.Client.Models;

namespace AlorClient;

public class Reconnect : Message
{
    public ReconnectionType ReconnectionType { get; }
    public Reconnect(ReconnectionInfo reconnectionInfo)
    {
        ReconnectionType = reconnectionInfo.Type;
    }

}
