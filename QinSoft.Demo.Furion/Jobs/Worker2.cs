using Furion.Logging;

namespace QinSoft.Demo.Furion.Jobs
{
    public class Worker2 : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information("Worker2 Execute:" + DateTime.Now);
                await Task.Delay(1000);
            }
        }
    }
}
