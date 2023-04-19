namespace AlorClient
{
    internal class Token
    {
        public required string AccessToken { get; set; }
        public DateTimeOffset Created { get; private set; } = DateTimeOffset.Now;
    }
}
