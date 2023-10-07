using QinSoft.Core.Configure;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QinSoft.Demo.Job.Jobs
{
    public class TestJob : IJob
    {
        public TestJob(IConfiger configer)
        {

        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Delay(100);
            Console.WriteLine("Hello world");
        }
    }
}
