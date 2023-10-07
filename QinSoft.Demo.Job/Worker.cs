using Quartz.Impl;
using Quartz;
using QinSoft.Demo.Job.Jobs;
using Quartz.Spi;

namespace QinSoft.Demo.Job
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly JobFactory _jobFactory;

        public Worker(ILogger<Worker> logger, JobFactory jobFactory)
        {
            _logger = logger;
            _jobFactory = jobFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 创建调度器工厂
            var schedulerFactory = new StdSchedulerFactory();

            // 获取调度器实例
            var scheduler = await schedulerFactory.GetScheduler();
            scheduler.JobFactory = _jobFactory;

            // 启动调度器
            await scheduler.Start();

            // 创建一个作业实例
            var job = JobBuilder.Create<TestJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // 创建一个触发器实例
            var trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // 将作业和触发器关联到调度器
            await scheduler.ScheduleJob(job, trigger);
            stoppingToken.Register(async () =>
            {
                // 关闭调度器
                await scheduler.Shutdown();
            });
        }

        public class JobFactory : IJobFactory
        {
            private readonly IServiceProvider _serviceProvider;
            public JobFactory(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
            {
                return _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
            }

            public void ReturnJob(IJob job) { }
        }
    }
}