

using LiteDB;

namespace DataStorage
{
    internal class BaseData
    {
        public required long ForeignKey { get; set; }

        public required DateTimeOffset Timestamp { get; set; }
        public required DateTimeOffset ClientTimestamp { get; set; }
    }
}
