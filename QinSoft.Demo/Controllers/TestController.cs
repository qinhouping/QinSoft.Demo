using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QinSoft.Demo.BLL.Services;
using QinSoft.Demo.Common.Model.Response;
using SqlSugar;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        [HttpGet("test2")]
        public object Test2()
        {
            object a = DateTime.Now;
            string b = JsonSerializer.Serialize(a);
            object c = JsonSerializer.Deserialize<object>(b);
            return c;
        }
    }
}
