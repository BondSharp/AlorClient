using Microsoft.Extensions.Hosting;

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
                var timeLeft = await GetTimeLeft();
                await Task.Delay(timeLeft, stoppingToken);
                await tokenAuthorization.UpdateToken();
            }
        }

        private async Task<TimeSpan> GetTimeLeft()
        {
            var token = await tokenAuthorization.TokenAsync();
            var leftTime = expirationTime - (DateTimeOffset.Now - token.Created);

            if (TimeSpan.Zero > leftTime)
            {
                return TimeSpan.Zero;
            }
            return leftTime;
        }
    }
}
