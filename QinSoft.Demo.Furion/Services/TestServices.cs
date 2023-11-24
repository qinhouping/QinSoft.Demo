using Furion.ConfigurableOptions;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Furion.Schedule;
using Furion.TimeCrontab;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QinSoft.Demo.Furion.Jobs;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace QinSoft.Demo.Furion.Services
{
    /// <summary>
    /// 测试服务
    /// </summary>
    [ApiDescriptionSettings("1Test")]
    [DynamicApiController]
    [ServiceFilter(typeof(LogActionFilterAttribute))]
    public class TestService
    {
        /// <summary>
        /// 测试Http
        /// </summary>
        [HttpGet]
        public async Task<string> GetHttp()
        {
            var response = await App.GetService<IApiHttp>().IndexAsync();
            return response;
        }

        /// <summary>
        /// 测试Config
        /// </summary>
        [HttpGet]
        public string GetConfig()
        {
            return App.Configuration["test"];
        }

        /// <summary>
        /// 测试Options
        /// </summary>
        [HttpGet]
        public OutResult GetOptions()
        {
            return App.GetOptions<TestOptions>().Adapt<OutResult>();
        }


        /// <summary>
        /// 测试Json
        /// </summary>
        [HttpGet]
        public string GetJson()
        {
            return JSON.Serialize(new { Time = DateTime.Now });
        }

        /// <summary>
        /// 测试HttpContext
        /// </summary>
        [HttpGet]
        public string GetIp()
        {
            return App.HttpContext.GetRemoteIpAddressToIPv4();
        }

        /// <summary>
        /// 测试脱敏
        /// </summary>
        [HttpGet]
        public OutResult GetSensitive()
        {
            return new OutResult() { AA = "坏人" };
        }

        /// <summary>
        /// 测试作业调度
        /// </summary>
        [HttpGet]
        public bool AddJob()
        {
            App.GetService<ISchedulerFactory>().AddJob<TestJob>("test-job", false, Triggers.Secondly());
            return true;
        }

        /// <summary>
        /// 测试作业调度HTTP
        /// </summary>
        [HttpGet]
        public bool AddHttpJob()
        {
            App.GetService<ISchedulerFactory>().AddHttpJob(request =>
            {
                request.RequestUri = "https://www.baidu.com";
                request.HttpMethod = HttpMethod.Get;
            }, "test-http-job", false, Triggers.Cron("* * * * * ?", CronStringFormat.WithSeconds));
            return true;
        }

        /// <summary>
        /// 测试作业调度
        /// </summary>
        [HttpDelete]
        public bool RemoveJob()
        {
            App.GetService<ISchedulerFactory>().RemoveJob("test-job");
            return true;
        }

        /// <summary>
        /// 测试作业调度HTTP
        /// </summary>
        [HttpDelete]
        public bool RemoveHttpJob()
        {
            App.GetService<ISchedulerFactory>().RemoveJob("test-http-job");
            return true;
        }

        /// <summary>
        /// 测试序列化
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object TestSer(IJsonSerializerProvider jsonSerializer)
        {
            object a = "hello";
            string b = JSON.Serialize(a);
            object c = JSON.Deserialize<object>(b);
            return c;
        }
    }

    [OptionsSettings("test_opt")]
    public class TestOptions : IConfigurableOptions
    {
        [MapSettings("a")]
        public string? A { get; set; }
    }

    public class OutResult
    {
        [SensitiveDetection('*')]
        public string AA { get; set; }
    }
}
