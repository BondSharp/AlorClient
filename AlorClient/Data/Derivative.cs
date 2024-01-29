namespace AlorClient;
public class Derivative : Security
{
    public DateTime Cancellation { get; set; }

    public required string UnderlyingSymbol { get; set; }
}
