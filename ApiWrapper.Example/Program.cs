using ApiWrapper.App;
using ApiWrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ApiWrapper.Example;

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



//await foreach (var r in Tester.Get())
//{
//Console.WriteLine(r);
//}

//class Tester
//{
//    public static async Task<int> Calculate(int a)
//    {
//        Console.WriteLine(a + 100);
//        await Task.Delay(1000);

//        return a + 100;
//    }
//    public static async IAsyncEnumerable<int> Get()
//    {
//        var i = 1;
//        while (true)
//        {
//            var result = await Calculate(i);
//            yield return i;
//            Console.WriteLine("next");
//            i++;
//            if (i == 5)
//            {
//                break;
//            }
//        }
//    }
//}