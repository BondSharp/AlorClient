namespace AlorClient
{
    public sealed class Settings
    {
        public required string RefreshToken { get; set; }
        public bool IsProduction { get; set; }
        public required Dictionary<string, string> CodesFutures { get; set; }
        public TimeSpan? ReconnectTimeout { get; set; }
        public TimeSpan? ErrorReconnectTimeout { get; internal set; }
        public TimeSpan RefreshingTokenTimeout { get; set; }
    }
}
