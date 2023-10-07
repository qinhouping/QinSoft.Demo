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
            // ��������������
            var schedulerFactory = new StdSchedulerFactory();

            // ��ȡ������ʵ��
            var scheduler = await schedulerFactory.GetScheduler();
            scheduler.JobFactory = _jobFactory;

            // ����������
            await scheduler.Start();

            // ����һ����ҵʵ��
            var job = JobBuilder.Create<TestJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // ����һ��������ʵ��
            var trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // ����ҵ�ʹ�����������������
            await scheduler.ScheduleJob(job, trigger);
            stoppingToken.Register(async () =>
            {
                // �رյ�����
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