using Microsoft.Extensions.DependencyInjection;

namespace QinSoft.Demo.Furion
{
    public class DALServiceComponent : IServiceComponent
    {
        public void Load(IServiceCollection services, ComponentContext componentContext)
        {
            services.AddRepositories();
            services.AddRemoteRequest(options =>
            {
                options.AddHttpClient("Baidu", c =>
                {
                    c.BaseAddress = new Uri("https://www.baidu.com/");
                });
            });
        }
    }
}
