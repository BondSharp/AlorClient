
namespace AlorClient
{
    internal class Reconnect
    {
        public DateTimeOffset Timestamp { get; }

        public Reconnect()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }
    }
}
