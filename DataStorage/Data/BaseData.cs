

using LiteDB;

namespace DataStorage
{
    internal class BaseData
    {
        [BsonId(true)]
        public long PrimeKey { get; set; }

        public required long ForeignKey { get; set; }

        public required DateTimeOffset Timestamp { get; set; }
        public required DateTimeOffset ClientTimestamp { get; set; }
    }
}
