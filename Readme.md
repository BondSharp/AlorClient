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
  },
  "Monitoring" : {
	"Symbols" : ["SBER"],
	"Futures" : ["SR"],
	"Options" : ["SR"]
  }		
}
```

## AlorClient

- RefreshToken получить можно по [ссылки](https://alor.dev/open-api-tokens)
- IsProduction являеться ли RefreshToken от production
- ReconnectTimeout время ожидания сообщение от WebSocket
- ErrorReconnectTimeout время ожидания для повторно соедение с WebSocket
- RefreshingTokenTimeout  время ожидания обновления token