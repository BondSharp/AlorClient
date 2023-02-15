using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiWrapper
{
    public abstract class Subscription
    {
        [JsonPropertyName("opcode")]
        public string OperationCode { get; }

        [JsonPropertyName("guid")]
        public Guid Guid { get; }

        [JsonPropertyName("token")]
        public string? Token { get; internal set; }

        protected Subscription(string operationCode)
        {
            OperationCode = operationCode;
            Guid = Guid.NewGuid();
        }

        protected Subscription(string operationCode, Guid guid)
        {
            OperationCode = operationCode;
            Guid = guid;
        }
    }

    public abstract class SecuritySubscription : Subscription
    {
        [JsonIgnore]
        public Security Security { get; }

        [JsonPropertyName("code")]
        public string Code => Security.Symbol;

        [JsonPropertyName("exchange")]
        public string Exchange => Security.Exchange;

        protected SecuritySubscription(Security security, string operationCode) : base(operationCode)
        {
            Security = security;
        }


    }

    public sealed class OrderBookSubscription : SecuritySubscription
    {
        [JsonPropertyName("depth")]
        public int Depth { get; }

        public OrderBookSubscription(Security security, int depth) : base(security, "OrderBookGetAndSubscribe")
        {
            Depth = depth;
        }
    }

    public sealed class AllDealsSubscription : SecuritySubscription
    {
        [JsonPropertyName("includeVirtualTrades\"")]
        public bool IncludeVirtualTrades { get; }

        [JsonPropertyName("depth")]
        public int Depth { get; }

        public AllDealsSubscription(Security security, bool includeVirtualTrades, int depth) : base(security, "AllTradesGetAndSubscribe")
        {
            IncludeVirtualTrades = includeVirtualTrades;
            Depth = depth;
        }

        public AllDealsSubscription(Security security) : this(security, false, 0)
        {

        }
    }


    internal sealed class UnSubscription : Subscription
    {
        [JsonIgnore]
        public Subscription Subscription { get; }

        public UnSubscription(Subscription subscription) : base("unsubscribe", subscription.Guid)
        {
            Subscription = subscription;
        }


    }
}
