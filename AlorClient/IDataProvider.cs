namespace AlorClient;

public interface IDataProvider
{
    IObservable<Message> Messages { get; }
}
