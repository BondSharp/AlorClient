

namespace AlorClient;
public interface ISubscriptions
{
    IObservable<Message> CreateMessages(RequestMessages request);
}
