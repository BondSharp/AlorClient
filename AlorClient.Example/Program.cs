using AlorClient;
using Common;
using AlorClient.Example;
using DataStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reactive.Linq;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services
                .AddAlorClient(config)
                .AddData()
                .AddDataStorage()
                .AddScoped<SimpleSubscription>()
                .AddScoped<SimpleEchoMessage>()
                .AddScoped<SimpleDataWriter>()
                ;
            }).Build();
host.Start();

var simpleSubscription = host.Services.GetRequiredService<SimpleSubscription>();
var simpleEchoMessage = host.Services.GetRequiredService<SimpleEchoMessage>();
var simpleDataWriter = host.Services.GetRequiredService<SimpleDataWriter>();

simpleEchoMessage.Echo();
simpleDataWriter.Write();
simpleSubscription.Subscribe();

host.WaitForShutdown();
