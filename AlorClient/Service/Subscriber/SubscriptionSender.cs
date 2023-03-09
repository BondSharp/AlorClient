using System.Text.Json;
using Websocket.Client;

namespace AlorClient
{
    internal class SubscriptionSender
    {
        private readonly TokenAuthorization tokenAuthorization;
        private readonly IWebsocketClient client;

        public SubscriptionSender(TokenAuthorization tokenAuthorization, IWebsocketClient client)
        {
            this.tokenAuthorization = tokenAuthorization;
            this.client = client;
        }

        public  void Send(Subscription subscription)
        {
            var token =  tokenAuthorization.Token();
            var message = ToJson(subscription, token);
            client.Send(message);
        }

        private string ToJson(Subscription subscription, Token token)
        {
            subscription.Token = token.AccessToken;
            return JsonSerializer.Serialize(subscription, subscription.GetType());
        }
    }
}
