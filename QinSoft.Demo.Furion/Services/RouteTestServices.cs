using Microsoft.AspNetCore.Mvc;

namespace QinSoft.Demo.Furion.Services
{
    /// <summary>
    /// 测试服务
    /// </summary>
    [ApiDescriptionSettings("1Test", Tag = "路由相关测试")]
    [DynamicApiController]
    [ServiceFilter(typeof(LogActionFilterAttribute))]
    //[Route("test3")]
    public class RouteTestApi
    {
        [Route("")]
        [HttpPost("")]
        [HttpPut("[action]")]
        public string GetTestMethod()
        {
            return "hello world";
        }
    }

    [ApiDescriptionSettings("1Test", Name = "RouteTest2", Tag = "路由相关测试")]
    public class RouteTestController : ControllerBase
    {
        [Route("my_test3")]
        [HttpPost]
        public string GetTestMethod()
        {
            return "hello world";
        }
    }
}
