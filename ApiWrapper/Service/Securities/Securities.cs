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

        public IAsyncEnumerable<Share> Shares(string? query = null)
        {
            var shares = Request<Share>(new SecuritiesRequest()
            {
                Cficode = "E",
                Query = query
            });

            return shares.Where(Filter);               ;
        }

        private bool Filter(Share share)
        {
            return share.Board == "TQBR" && (share.CfiCode.StartsWith("ES") || share.CfiCode.StartsWith("EP"));
        }

        public IAsyncEnumerable<Future>  Futures(string? query = null)
        {
            var futures = Request<Future>(new SecuritiesRequest()
            {
                Cficode = "F",
                Query = query
            });

            return futures;
        }

        public IAsyncEnumerable<Option> Options(string? query = null)
        {
            var options =  Request<Option>(new SecuritiesRequest()
            {
                Cficode = "O",
                Query = query
            });

            return options;
        }


        private async IAsyncEnumerable<T> Request<T>(SecuritiesRequest @params)
        {
            while (true)
            {
                var securities = await alorApi.Get<T[]>(path, @params);
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
