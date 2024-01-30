using AlorClient.Service.WebSocket.DataProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Websocket.Client;

namespace AlorClient;

public static class Extentoions
{
    private const string configurationKey = "AlorClient";
    public static IServiceCollection AddAlorClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var settings = configuration.GetRequiredSection(configurationKey).Get<Settings>()
            ?? throw new Exception($"Not found configuration wthi key '{configurationKey}'");

        return serviceCollection
            .AddSingleton(settings)
            .AddSingleton<TokenAuthorization>()
            .AddHostedService<UpdatingToken>()
            .AddRest()
            .AddSubscriber();
    }

    private static IServiceCollection AddRest(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<AlorApi>()
            .AddSingleton<ISecurities, Securities>()
              .AddSingleton<IDeals, Deals>();
    }

    private static IServiceCollection AddSubscriber(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<WebSocketClientFactory>()
             .AddTransient<IMarkerDataBuilder, MarkerDataBuilder>()
            .AddScoped<SubscriptionSender>()
            .AddScoped<SubscriptionCollection>()
            .AddScoped<Subscriber>()
            .AddScoped(GetWebsocketClient)
            .AddScoped<ReconnectProvider>()
            .AddScoped<MessageProvider>()
            .AddScoped<MessageProvider>();
    }

    private static IWebsocketClient GetWebsocketClient(IServiceProvider serviceProvider) => serviceProvider.GetRequiredService<WebSocketClientFactory>().Factory();
}
