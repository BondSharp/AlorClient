namespace AlorClient
{
    public interface ISubscriber
    {
        IDataProvider DataProvider { get; }
        void Subscribe(Subscription subscription);
        void UnSubscribe(Subscription subscription);
    }
}
