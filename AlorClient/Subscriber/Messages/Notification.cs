namespace AlorClient;

internal class Notification : Message
{
    public Subscription Subscription { get; }
    public int Code { get; }
    public string Message { get; }

    public Notification(int code, string message, Subscription subscription)
    {
        Code = code;
        Message = message;
        Subscription = subscription;
    }

    public override string ToString()
    {
        return $"Code : {Code}, Message: {Message} Guid {Subscription.Guid}";
    }
}
