

namespace AlorClient;
public interface IMarkerDataBuilder
{
    IMarkerDataBuilder OnOrderBook(Security security, int depth, int frequency);
    IMarkerDataBuilder OnDeals(Security security, int depth, int frequency);
    IObservable<SecurityMessage> Build();
}
