using System.Text.Json.Serialization;

namespace AlorClient;

public abstract class Subscription
{
    [JsonPropertyName("opcode")]
    public string OperationCode { get; }

    [JsonPropertyName("guid")]
    public Guid Guid { get; }

    [JsonPropertyName("token")]
    public string? Token { get; internal set; }

    protected Subscription(string operationCode)
    {
        OperationCode = operationCode;
        Guid = Guid.NewGuid();
    }

    protected Subscription(string operationCode, Guid guid)
    {
        OperationCode = operationCode;
        Guid = guid;
    }
}
