
using AlorClient.Domain;

namespace AlorClient;

public class DealMessage : SecurityMessage
{
    public Deal Deal { get; }

    internal DealMessage(Security security, Deal deal): base(security)
    {
        Deal = deal;
    }

    public override string ToString()
    {
        return $"{base.ToString()} {Deal}";
    }
}
