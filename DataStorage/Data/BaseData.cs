

using LiteDB;

namespace DataStorage
{
    internal class BaseData
    {
        [BsonRef]
        public required long ForeignKey { get; set; }

        public required DateTimeOffset Timestamp { get; set; }
        public required DateTimeOffset ClientTimestamp { get; set; }
    }
}
