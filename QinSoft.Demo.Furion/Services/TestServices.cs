using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QinSoft.Demo.BLL.Services;
using QinSoft.Demo.Common.Model.Response;

namespace QinSoft.Demo.Furion.Services
{
    [DynamicApiController]
    public class TestService
    {
        private ITestService testService;

        public TestService(ITestService testService)
        {
            this.testService = testService;
        }

        [HttpGet]
        public List<Project> Get()
        {
            return testService.GetProjects();
        }
    }
}
