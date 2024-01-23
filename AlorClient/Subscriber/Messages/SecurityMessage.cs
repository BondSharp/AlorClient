namespace AlorClient;

public abstract class SecurityMessage : Message
{
    public Security Security { get; }

    public SecurityMessage(Security security) {
        Security = security;
    }

    public override string ToString()
    {
        return Security.ToString();
    }
}
