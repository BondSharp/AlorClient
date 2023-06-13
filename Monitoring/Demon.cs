using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Monitoring
{
    internal class Demon : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public Demon(
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (stoppingToken.IsCancellationRequested)
            {
                await RunAsync(stoppingToken);
            }
        }

        async Task RunAsync(CancellationToken stoppingToken)
        {
            var finish = GetFinishDateTime();

            using (var scope = serviceProvider.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<AutoSubscriber>().Subscribe();
                var disposable = scope.ServiceProvider.GetRequiredService<Tracker>().Tracke();
                while (stoppingToken.IsCancellationRequested && finish > DateTimeOffset.Now)
                {
                    await Task.Delay(1000);
                }
                disposable.Dispose();
            }
        }

        private DateTimeOffset GetFinishDateTime()
        {
            var datetime = DateTimeOffset.Now;
            var result = datetime.Date + TimeSpan.FromHours(3);
            if (datetime > result)
            {
                return result.AddDays(1.0);
            }
            return result;
        }
    }
}
