using QinSoft.Demo.Furion.Services;

namespace QinSoft.Demo.Furion
{
    public class ApiStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfigurableOptions<TestOptions>();
            services.AddConfigurableOptions<SwaggerLoginOptions>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<>();
        }
    }
}
