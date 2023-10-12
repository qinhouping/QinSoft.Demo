using Furion.Logging;

namespace QinSoft.Demo.Furion.Jobs
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information("Worker Execute:" + DateTime.Now);
                await Task.Delay(1000);
            }
        }
    }
}
