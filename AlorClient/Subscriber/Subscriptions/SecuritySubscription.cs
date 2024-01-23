using System.Text.Json.Serialization;
using AlorClient.Domain;

namespace AlorClient;

internal abstract class SecuritySubscription : Subscription
{
    [JsonIgnore]
    public Security Security { get; }

    [JsonPropertyName("code")]
    public string Code => Security.Symbol;

    [JsonPropertyName("exchange")]
    public string Exchange => "MOEX";

    [JsonPropertyName("frequency")]
    public int Frequency { get; }

    protected SecuritySubscription(Security instrument, string operationCode,int frequency) : base(operationCode)
    {
        Security = instrument;
        Frequency = frequency;
    }


}
