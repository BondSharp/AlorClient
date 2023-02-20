using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWrapper
{
    internal class UpdatingToken : BackgroundService
    {
        private readonly TokenAuthorization tokenAuthorization;
        private readonly TimeSpan expirationTime;
        public UpdatingToken(TokenAuthorization tokenAuthorization)
        {
            expirationTime = TimeSpan.FromMinutes(10);
            this.tokenAuthorization = tokenAuthorization;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var timeLeft = GetTimeLeft();          
                await Task.Delay(timeLeft, stoppingToken);
                await tokenAuthorization.UpdateToken();
            }
        }

        private TimeSpan GetTimeLeft()
        {
            var token = tokenAuthorization.Token();
            var leftTime = expirationTime - (DateTimeOffset.Now - token.Created);

            if (TimeSpan.Zero > leftTime)
            {
                return TimeSpan.Zero;
            }
            return leftTime;
        }


    }
}
