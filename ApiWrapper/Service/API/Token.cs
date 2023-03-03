namespace ApiWrapper
{
    internal class Token
    {
        public string AccessToken { get; set; }
        public DateTimeOffset Created { get; private set; } = DateTimeOffset.Now;
    }
}
