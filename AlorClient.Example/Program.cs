using AlorClient;
using Common;
using AlorClient.Example;
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
                .AddScoped<SimpleSubscription>()
                .AddScoped<SimpleEchoMessage>()
                ;
            }).Build();
host.Start();

var simpleSubscription = host.Services.GetRequiredService<SimpleSubscription>();
var simpleEchoMessage = host.Services.GetRequiredService<SimpleEchoMessage>();

simpleEchoMessage.Echo();
simpleSubscription.Subscribe();

host.WaitForShutdown();
