using AlorClient.Service.WebSocket.DataProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Websocket.Client;

namespace AlorClient
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
                    .AddSingleton<TokenAuthorization>()
                    .AddHostedService<UpdatingToken>()
                    .AddSecurities()
                    .AddSubscriber();
        }

        private static IServiceCollection AddSecurities(this IServiceCollection serviceCollection)
        {
            return serviceCollection
             .AddSingleton<ISecurities, Securities>()
             .AddSingleton<SecuritiesApi>();
        }

        private static IServiceCollection AddSubscriber(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<WebsocketClientFactory>()
                .AddScoped<SubscriptionSender>()
                .AddScoped<SubscriptionCollection>()
                .AddScoped<ISubscriber, Subscriber>()
                .AddScoped(GetWebsocketClient)
                .AddScoped<ReconnectProvider>()
                .AddScoped<NotificationProvider>()
                .AddScoped<MessageProvider>()
                .AddScoped<MessageProvider>()
                .AddScoped<DataProvider>();
        }

        private static IWebsocketClient GetWebsocketClient(IServiceProvider serviceProvider) => serviceProvider.GetRequiredService<WebsocketClientFactory>().Factory();
    }
}
