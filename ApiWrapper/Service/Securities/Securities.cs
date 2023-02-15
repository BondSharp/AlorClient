namespace ApiWrapper
{
    internal class Securities : SecuritieIntarface
    {
        private const string path = "/md/v2/Securities";
        private readonly AlorApi alorApi;

        static string[] shareCfiCodes = new string[] { "" };


        public Securities(AlorApi alorApi)
        {
            this.alorApi = alorApi;
        }

        public Share[] Shares(string? query = null)
        {
            var shares = Request<Share>(new SecuritiesRequest()
            {
                Cficode = "E",
                Query = query
            });

            return shares
                .Where(Filter)
                .ToArray();
        }

        private bool Filter(Share share)
        {
            return share.Board == "TQBR" && (share.CfiCode.StartsWith("ES") || share.CfiCode.StartsWith("EP"));
        }

        public Future[] Futures(string? query = null)
        {
            var futures = Request<Future>(new SecuritiesRequest()
            {
                Cficode = "F",
                Query = query
            });

            return futures

                .ToArray();
        }

        public Option[] Options(string? query = null)
        {
            var futures = Request<Option>(new SecuritiesRequest()
            {
                Cficode = "O",
                Query = query
            });

            return futures.ToArray();
        }


        private IEnumerable<T> Request<T>(SecuritiesRequest @params)
        {
            while (true)
            {
                var securities = alorApi.Get<T[]>(path, @params).Result;
                foreach (var security in securities)
                {
                    yield return security;
                }
                @params.Offset += securities.Length;
                if (securities.Length < @params.Limit)
                {
                    break;
                }
            }
        }

    }
}
