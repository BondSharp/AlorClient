

namespace AlorClient;
public interface IMarkerDataBuilder
{
    IMarkerDataBuilder OrderBook(Security security, int depth, int frequency);
    IMarkerDataBuilder Deals(Security security, int depth, int frequency);
    IObservable<Message> Build();
}
