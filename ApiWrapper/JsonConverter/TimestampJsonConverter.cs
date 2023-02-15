using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiWrapper.JsonConverter
{
    public class TimestampJsonConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value =  reader.GetInt64();
            return DateTimeOffset.FromUnixTimeMilliseconds(value);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            var unixTimeMilliseconds = value.ToUnixTimeMilliseconds();
            writer.WriteNumberValue(unixTimeMilliseconds);
        }
    }
}
