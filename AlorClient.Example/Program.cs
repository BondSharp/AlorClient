using AlorClient;
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
                services.AddAlorClient(config);
            }).Build();
host.Start();

var securities = host.Services.GetRequiredService<ISecurities>();
var subscriber = host.Services.GetRequiredService<ISubscriber>();
var dataProvider = subscriber.DataProvider;
dataProvider.Messages.OfType<SecurityMessage>().Subscribe(securityMessage =>
{
    string? textMessage = null;
    var symbol = securityMessage.SecuritySubscription.Security.Symbol;

    if (securityMessage is DealMessage dealMessage)
    {
        textMessage = $"deal with price {dealMessage.Deal.Price}";
    }

    if (securityMessage is OrderBookMessage orderBookMessage)
    {
        var ask = orderBookMessage.OrderBook.Asks
            .Select(ask => ask.Price)
            .OrderByDescending(price => price)
            .FirstOrDefault();

        var bid = orderBookMessage.OrderBook.Bids
           .Select(ask => ask.Price)
           .OrderBy(price => price)
           .FirstOrDefault();

        textMessage = $"orderBook with ask {ask} and bid {bid}";
    }

    Console.WriteLine($"Received a message '{textMessage}' for {symbol}");
});

var share = await securities.GetShareAsync("SBER");
subscriber.Subscribe(new OrderBookSubscription(share, 3));
subscriber.Subscribe(new DealsSubscription(share));


var nowDate = DateTimeOffset.Now.Date;

var future = await securities.GetFuturesAsync(share)
   .Where(x => x.ExpirationDate > nowDate)
   .OrderBy(x => x.ExpirationDate)
   .FirstAsync();
subscriber.Subscribe(new OrderBookSubscription(future, 3));
subscriber.Subscribe(new DealsSubscription(future));

var lastDeal = await securities.GetLastDealAsync(share);
var OptionsBoard = await securities.GetOptionsBoardsAsync(share)
   .Where(optionsBoard => optionsBoard.ExpirationDate > nowDate)
   .OrderBy(optionsBoard => optionsBoard.ExpirationDate)
   .ThenBy(optionsBoard => Math.Abs(optionsBoard.Strike - lastDeal?.Price ?? 0))
   .FirstAsync();
subscriber.Subscribe(new OrderBookSubscription(OptionsBoard.Call, 20));
subscriber.Subscribe(new DealsSubscription(OptionsBoard.Call));
subscriber.Subscribe(new OrderBookSubscription(OptionsBoard.Put, 20));
subscriber.Subscribe(new DealsSubscription(OptionsBoard.Put));

host.WaitForShutdown();

