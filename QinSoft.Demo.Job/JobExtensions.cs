using QinSoft.Core.Data.Database;
using QinSoft.Demo.DAL.Repository.Impl;
using QinSoft.Demo.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz.Spi;
using static QinSoft.Demo.Job.Worker;
using QinSoft.Demo.Job.Jobs;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JobExtensions
    {
        public static void AddJobs(this IServiceCollection services)
        {
            services.TryAddSingleton<JobFactory>();
            services.TryAddSingleton<TestJob>();
        }
    }
}
