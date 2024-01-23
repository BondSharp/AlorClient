using System.Text.Json.Serialization;

namespace AlorClient;

public class Security
{
    public required string Symbol { get; set; }

    public required string ShortName { get; set; }

    public required string Exchange { get; set; }

    public required string CfiCode { get; set; }

    public required string Board { get; set; }

    public DateTime Cancellation { get; set; }

    public override string ToString()
    {
        return Symbol;
    }
}
