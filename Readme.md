# Библиотека для работы с [Alor Open Api](https://alor.dev/docs)  на языке C#
## Пример appsettings.json

```json
{
  "AlorClient" : {  
	"RefreshToken" : "4876c5f4-b51b-4ae2-99d2-d8df6bfe22d5",
	"IsProduction" : true,
	"ReconnectTimeout" : "0:01:00",
	"ErrorReconnectTimeout" : "0:01:00",
	"RefreshingTokenTimeout" : "0:10:00"
  }	
}
```

## AlorClient

- RefreshToken получить можно по [ссылки](https://alor.dev/open-api-tokens)
- IsProduction являеться ли RefreshToken от production
- ReconnectTimeout время ожидания сообщение от WebSocket
- ErrorReconnectTimeout время ожидания для повторно соедение с WebSocket
- RefreshingTokenTimeout  время ожидания обновления token

## Пример подключение библиотеки 
```C#
var config = new ConfigurationBuilder()
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

var services =  host.Services;
```

## Работа с rest

```C#
var alorClient = services.GetRequiredService<IAlorClient>();

var sber = await alorClient.GetSecurities(TimeSpan.FromDays(1)).OfType<Share>().FirstAsync(x => x.Symbol == "SBER");

await alorClient.GetHistoryDeals(sber, 5000, null).ToArrayAsync();
await alorClient.GetHistoryDeals(sber, 1, null).FirstAsync(); 

var allDeals = await alorClient.GetAllDeals(sber, 5000, null).ToArrayAsync();
await alorClient.GetAllDeals(sber, 5000, allDeals.Last()).ToArrayAsync();

```

## Маркер дата в реальном времени 

``` C#
services.GetRequiredService<IMarkerDataBuilder>()
    .OnOrderBook(sber,20,0)
    .OnDeals(sber,20,0)
    .Build()
    .Subscribe(message=>Console.WriteLine(message), exception=> Console.WriteLine(exception.Message));
```