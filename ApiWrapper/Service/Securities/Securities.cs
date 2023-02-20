using Microsoft.AspNetCore.Http.Extensions;

namespace ApiWrapper
{
    internal class Securities : SecuritiesIntarface
    {
        private const string path = "/md/v2/Securities";
        private readonly AlorApi alorApi;

        static string[] shareCfiCodes = new string[] { "" };


        public Securities(AlorApi alorApi)
        {
            this.alorApi = alorApi;
        }

        public async IAsyncEnumerable<T> Get<T>(string? query = null) where T : Security
        {

            var cficode = GetCficode<T>();
            await foreach (var security in Request<T>(cficode, query))
            {
                if (security is Share share && share.Board == "TQBR" && (share.CfiCode.StartsWith("ES") || share.CfiCode.StartsWith("EP")))
                {
                    yield return security;
                }

                if (security is Option)
                {
                    yield return security;
                }

                if (security is Future)
                {
                    yield return security;
                }

            };
        }

        private string GetCficode<T>()
        {
            switch (typeof(T).Name)
            {
                case nameof(Share):
                    return "E";
                case nameof(Future):
                    return "F";
                case nameof(Option):
                    return "O";
                default:
                    throw new ArgumentException(nameof(T));
            }
        }

        private async IAsyncEnumerable<T> Request<T>(string Cficode, string? query)
        {
            var limit = 20;
            var offset = 0;
            var @params = new Dictionary<string, string>
            {
                { "cficode", Cficode },
                { "limit", limit.ToString() },
                { "exchange", "MOEX" }
            };
            if (query != null)
            {
                @params.Add("query", query);
            }
            do
            {
                @params["offset"] = offset.ToString();
                var securities = await alorApi.Get<T[]>(path, new QueryBuilder(@params));
                foreach (var security in securities)
                {
                    yield return security;
                }
                offset += securities.Length;
            } while (offset < limit);
        }


    }
}
