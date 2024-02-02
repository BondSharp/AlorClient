using System.Text;
using System.Text.Json.Serialization;
using AlorClient.Domain;

namespace AlorClient;

public class OrderBook
{
    [JsonPropertyName("bids")]
    public required Offer[] Bids { get; set; }

    [JsonPropertyName("asks")]
    public required Offer[] Asks { get; set; }

    [JsonPropertyName("ms_timestamp")]
    [JsonConverter(typeof(TimestampJsonConverter))]
    public DateTimeOffset Timestamp { get; set; }

    [JsonPropertyName("existing")]
    public bool Existing { get; set; }

    public override string ToString()
    {
        var strings = new StringBuilder();
        var bids = string.Join(',', Bids.Select(x => x.ToString()));
        var asks = string.Join(',', Asks.Select(x => x.ToString()));
        return $" {Timestamp} {Existing} [{bids}],[{asks}]";
    }
}
