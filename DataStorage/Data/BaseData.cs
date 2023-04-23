

using LiteDB;

namespace DataStorage
{
    internal class BaseData
    {
        [BsonRef]
        public required Security Security { get; set; }
    }
}
