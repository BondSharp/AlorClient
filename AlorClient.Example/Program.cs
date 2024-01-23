using AlorClient;
using AlorClient.Domain;
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
                ;
            }).Build();
host.Start();

var securities =  host.Services.GetRequiredService<ISecurities>();
var stocks = await securities.GetSharesAsync();
var sber = stocks.First(x => x.Symbol == "SBER");

var subscriptions = host.Services.GetRequiredService<ISubscriptions>();

subscriptions.CreateMessages(new RequestMessages(sber)).Subscribe(Console.WriteLine);

host.WaitForShutdown();