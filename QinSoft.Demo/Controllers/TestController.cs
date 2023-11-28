using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QinSoft.Demo.BLL.Services;
using QinSoft.Demo.Common.Model.Response;
using SqlSugar;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QinSoft.Demo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
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

        /// <summary>
        /// 测试
        /// </summary>
        [HttpPost]
        [Authorize("test", Roles = "Admin")]
        public Project TestMethod([FromBody] Project project)
        {
            return project;
        }

        /// <summary>
        /// 测试异常
        /// </summary>
        [HttpGet]
        public Project TestExceptionMethod()
        {
            throw new InvalidProgramException();
        }
    }
}
