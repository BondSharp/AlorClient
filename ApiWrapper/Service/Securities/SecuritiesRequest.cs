namespace ApiWrapper
{
    internal class SecuritiesRequest
    {
        public required string Cficode { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; } = 100;
        public string? Query { get; set; }

        public string Exchange { get; set; } = "MOEX";
    }
}
