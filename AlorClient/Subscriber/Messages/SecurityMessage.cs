namespace AlorClient
{
    public abstract class SecurityMessage : Message
    {
        public SecuritySubscription SecuritySubscription { get; }

        public SecurityMessage(SecuritySubscription securitySubscription) {
            SecuritySubscription = securitySubscription;
        } 
    }
}
