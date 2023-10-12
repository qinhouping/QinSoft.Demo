using Microsoft.Extensions.Logging;
using QinSoft.Demo.Furion.Services;

namespace QinSoft.Demo.Furion
{
    public class ApiComponent : IServiceComponent
    {
        public void Load(IServiceCollection services, ComponentContext componentContext)
        {
            services.AddFileLogging(options =>
            {
                options.FileNameRule = fileName => string.Format(fileName, DateTime.Now);
            });
            services.AddRemoteRequest(services =>
            {
                services.AddHttpClient("Baidu", client =>
                {
                    client.BaseAddress = new Uri("https://www.baidu.com/");
                    client.DefaultRequestHeaders.Add("test_token", "test_token");
                });
            });
            services.AddSensitiveDetection();
        }
    }
}
