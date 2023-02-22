namespace ApiWrapper
{
    public interface ISubscriber
    {
        IObservable<Message> Messages { get; }
        IObservable<Notification> Notifications { get; }
        void Subscribe(Subscription subscription);
        public void UnSubscribe(Subscription subscription);
    }
}
