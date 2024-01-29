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
var instruments = await securities.GetSecurities(TimeSpan.FromDays(1)).ToArrayAsync();

var sber = instruments.OfType<Share>().First(x => x.Symbol == "SBER");

host.Services.GetRequiredService<IMarkerDataBuilder>()
    .OnOrderBook(sber,20,0)
    .OnDeals(sber,20,0)
    .Build()
    .Subscribe(message=>Console.WriteLine(message), exception=> Console.WriteLine(exception.Message));



host.WaitForShutdown();