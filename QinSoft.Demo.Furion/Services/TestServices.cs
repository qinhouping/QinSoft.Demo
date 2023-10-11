using Furion.FriendlyException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QinSoft.Demo.BLL.Services;
using QinSoft.Demo.Common.Model.Response;

namespace QinSoft.Demo.Furion.Services
{
    /// <summary>
    /// 测试服务
    /// </summary>
    [DynamicApiController]
    public class TestService
    {
        private ITestService testService;

        public TestService(ITestService testService)
        {
            this.testService = testService;
        }

        /// <summary>
        /// 测试操作
        /// </summary>
        [HttpGet]
        [ServiceFilter(typeof(LogFilter))]
        public List<Project> Get()
        {
            var response = App.GetService<IHttp>().GetBaidu().Result;
            return testService.GetProjects();
        }
    }
}
