using QinSoft.Demo.Furion.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QinSoft.Demo.Furion
{
    public class JobComponent : IServiceComponent
    {
        public void Load(IServiceCollection services, ComponentContext componentContext)
        {
            services.AddHostedService<Worker>();
            services.AddHostedService<Worker2>();
        }
    }
}
