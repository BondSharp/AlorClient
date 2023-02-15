// See https://aka.ms/new-console-template for more information
using ApiWrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reactive.Linq;
using System.Text.Json;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var provider = new ServiceCollection()
    .AddApiWrapper(config)
    .BuildServiceProvider();



var result = provider.GetRequiredService<SecuritieIntarface>().Shares("Sber").First();


using (provider.CreateScope())
{
    var subscriber = provider.GetRequiredService<SubscriberIntarface>();
    //subscriber.Messages.Subscribe(m =>
    //{
    //    Console.WriteLine(DateTime.Now);
    //    Console.WriteLine(m);
    //});

    subscriber.Notifications.Subscribe(m =>
    {
        Console.WriteLine(DateTime.Now);
        Console.WriteLine(m);
    });

    var orderBookSubscribe = new OrderBookSubscription(result, 10);
    subscriber.Subscribe(orderBookSubscribe);
    var allDealsSubscription = new AllDealsSubscription(result);
    subscriber.Subscribe(allDealsSubscription);

    Console.ReadLine();
    subscriber.Subscribe(allDealsSubscription);
}
