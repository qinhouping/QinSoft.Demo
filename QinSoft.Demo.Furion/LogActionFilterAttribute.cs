using Furion.DependencyInjection;
using Furion.Logging;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QinSoft.Demo.Furion
{
    /// <summary>
    /// 日志动作过滤器
    /// </summary>
    public class LogActionFilterAttribute : Attribute, IAsyncActionFilter, IScoped
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Log.Information($"Action Execution: url{context.HttpContext.Request.Path}");

            await next.Invoke();
        }
    }
}
