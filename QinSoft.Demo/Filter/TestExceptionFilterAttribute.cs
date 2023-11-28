using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QinSoft.Demo.Api.Filter
{
    public class TestExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILogger _logger;
        public TestExceptionFilterAttribute(ILogger<TestExceptionFilterAttribute> logger)
        {
            this._logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, $"Action Execute Exception:{context.HttpContext.Request.GetDisplayUrl()} Message:{context.Exception.Message}");
            base.OnException(context);
        }
    }
}
