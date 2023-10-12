using Furion.ConfigurableOptions;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QinSoft.Demo.Furion.Services
{
    /// <summary>
    /// 测试服务
    /// </summary>
    [ApiDescriptionSettings("Test")]
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
