// See https://aka.ms/new-console-template for more information
using ApiWrapper.App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var provider = new ServiceCollection()
    .AddApiWrapper(config)
    .BuildServiceProvider();



var result = provider.GetRequiredService<TokenAuthorization>().Token().Result;
Console.WriteLine(result);
Console.ReadLine();