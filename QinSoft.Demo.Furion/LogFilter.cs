using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QinSoft.Demo.Furion
{
    /// <summary>
    /// Action过滤器
    /// </summary>
    public class LogFilter : IAsyncActionFilter, IScoped
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("begin");

            var result= await next.Invoke();

            Console.WriteLine("end");
        }
    }
}
