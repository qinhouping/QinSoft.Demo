using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QinSoft.Demo.Api.Filter
{
    public class TestActionFilterAttribute : ActionFilterAttribute
    {
        private ILogger _logger;
        public TestActionFilterAttribute(ILogger<TestActionFilterAttribute> logger)
        {
            this._logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Action Executing:{context.HttpContext.Request.GetDisplayUrl()}");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action Executed:{context.HttpContext.Request.GetDisplayUrl()}");
            base.OnActionExecuted(context);
        }
    }
}
