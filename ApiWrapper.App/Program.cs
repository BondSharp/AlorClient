// See https://aka.ms/new-console-template for more information
using ApiWrapper;
using ApiWrapper.App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();



await Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddApiWrapper(config);
                services.AddHostedService<Example>();
            }).RunConsoleAsync();
