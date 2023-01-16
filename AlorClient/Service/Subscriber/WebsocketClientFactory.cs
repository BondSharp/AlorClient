using Websocket.Client;

namespace AlorClient
{
    internal class WebsocketClientFactory
    {
        private const string DevelopmentAddress = "wss://apidev.alor.ru/ws";
        private const string ProductionAddress = "wss://api.alor.ru/ws";

        private readonly Settings settings;

        public WebsocketClientFactory(Settings settings)
        {
            this.settings = settings;
        }

        public IWebsocketClient Factory()
        {
            var uri = new Uri(settings.IsProduction ? ProductionAddress : DevelopmentAddress);
            var websocketClient = new WebsocketClient(uri);
            websocketClient.ReconnectTimeout = settings.ReconnectTimeout;
            websocketClient.ErrorReconnectTimeout = settings.ErrorReconnectTimeout;
            websocketClient.Start();
            return websocketClient;
        }
    }
}
