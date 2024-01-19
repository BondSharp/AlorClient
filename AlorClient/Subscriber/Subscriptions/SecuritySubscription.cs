using System.Text.Json.Serialization;
using AlorClient.Domain;

namespace AlorClient;

public abstract class SecuritySubscription : Subscription
{
    [JsonIgnore]
    public Instrument Instrument { get; }

    [JsonPropertyName("code")]
    public string Code => Instrument.Symbol;

    [JsonPropertyName("exchange")]
    public string Exchange => "MOEX";

    protected SecuritySubscription(Instrument instrument, string operationCode) : base(operationCode)
    {
        Instrument = instrument;
    }


}
