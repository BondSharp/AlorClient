# Библиотека для работы с [Alor Open Api](https://alor.dev/docs)  на языке C#
## Пример appsettings.json

```json
{
  "ApiWrapper" : {  
	"RefreshToken" : "4876c5f4-b51b-4ae2-99d2-d8df6bfe22d5",
	"IsProduction" : true,
	"ReconnectTimeout" : "0:01:00",
	"ErrorReconnectTimeout" : "0:01:00",
	"RefreshingTokenTimeout" : "0:10:00",
	"CodesFutures" : {
		"SBER" : "SR"
	}
  }
}
```

- RefreshToken получить можно по [ссылки](https://alor.dev/open-api-tokens)
- IsProduction являеться ли RefreshToken от боевого сервера
- ReconnectTimeout время ожидания сообщение от веб-сокет
- ErrorReconnectTimeout время ожидания для повторно соедение с веб-сокет
- CodesFutures [словарь коротких кодов для фьючерсов](https://www.moex.com/s205)
- RefreshingTokenTimeout  время ожидания обновления токена