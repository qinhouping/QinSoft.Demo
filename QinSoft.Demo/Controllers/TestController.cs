using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QinSoft.Demo.BLL.Services;
using QinSoft.Demo.Common.Model.Response;
using SqlSugar;

namespace QinSoft.Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private ITestService testService;

        public TestController(ITestService testService)
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
