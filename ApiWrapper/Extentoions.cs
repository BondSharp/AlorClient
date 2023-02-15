using ApiWrapper.Service.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Websocket.Client;

namespace ApiWrapper
{
    public static class Extentoions
    {
        public static IServiceCollection AddApiWrapper(this IServiceCollection serviceCollection, IConfiguration configuration, string configurationKey = "ApiWrapper")
        {
            var settings = configuration.GetRequiredSection(configurationKey).Get<Settings>()
                ?? throw new Exception($"Not found configuration wthi key '{configurationKey}'");
            return serviceCollection.AddApiWrapper(settings);
        }

        public static IServiceCollection AddApiWrapper(this IServiceCollection serviceCollection, Settings settings)
        {
            return serviceCollection
                    .AddSingleton(settings)
                    .AddSingleton<AlorApi>()
                    .AddSingleton<SecuritieIntarface, Securities>()
                    .AddSingleton<TokenAuthorization>()
                    .AddSingleton<WebsocketClientFactory>()
                    .AddScoped(GetWebsocketClient)
                    .AddScoped<SubscriptionSender>()
                    .AddScoped<MessageProvider>()
                    .AddScoped<NotificationProvider>()
                    .AddScoped<SubscriptionCollection>()
                    .AddScoped<WebsocketReconnect>()
                    .AddScoped<SubscriberIntarface, Subscriber>();
        }

        private static IWebsocketClient GetWebsocketClient(IServiceProvider serviceProvider) => serviceProvider.GetRequiredService<WebsocketClientFactory>().Factory();
    }
}
