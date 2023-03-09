namespace AlorClient
{
    public interface ISubscriber
    {
        IDataProvider DataProvider { get; }
        void Subscribe(Subscription subscription);
        public void UnSubscribe(Subscription subscription);
    }
}
