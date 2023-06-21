using AlorClient;
using Common;
using DataStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reactive.Linq;
using Monitoring;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
.Build();

var configuration = config.GetRequiredSection("Monitoring").Get<Configuration>()
       ?? throw new Exception($"Not found configuration wthi key Monitoring");

var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services
                .AddAlorClient(config)
                .AddData()
                .AddDataStorage()
                .AddSingleton(configuration)
                .AddHostedService<Demon>()
                .AddScoped<AutoSubscriber>()
                .AddScoped<Tracker>()
                ;
            }).Build();
host.Start();


await host.WaitForShutdownAsync();
