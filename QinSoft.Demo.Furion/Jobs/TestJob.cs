using Furion.DependencyInjection;
using Furion.Logging;
using Furion.Schedule;

namespace QinSoft.Demo.Furion.Jobs
{
    public class TestJob : IJob, ISingleton
    {
        public TestJob(ILogger<TestJob> logger)
        {

        }

        public Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {
            Log.Information("TestJob Execute:" + DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
