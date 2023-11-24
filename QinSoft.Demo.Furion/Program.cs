// See https://aka.ms/new-console-template for more information

using Furion.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Unicode;
using Furion.FriendlyException;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Furion.DataValidation;
using Microsoft.AspNetCore.Mvc.Formatters;

Serve.Run(RunOptions.Default
    .WithArgs(args)
    .AddComponent<ServeServiceComponent>()
    .AddComponent<ApiComponent>()
    .AddComponent<JobComponent>()
    .UseComponent<ServeApplicationComponent>());


[SuppressSniffer]
public sealed class ServeServiceComponent : IServiceComponent
{
    /// <summary>
    /// 装载服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="componentContext"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Load(IServiceCollection services, ComponentContext componentContext)
    {
        // 控制台日志美化
        services.AddConsoleFormatter();

        // 配置跨域
        services.AddCorsAccessor();

        services.AddMvc(options =>
        {
            options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
            options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
        });

        // 控制器和规范化结果
        services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                })
                .AddInjectWithUnifyResult<RestResultProvider>()
                .AddJsonOptions(options =>
                {
                    //options.JsonSerializerOptions.DefaultBufferSize = 10_0000;//返回较大数据数据序列化时会截断，原因：默认缓冲区大小（以字节为单位）为16384。
                    options.JsonSerializerOptions.Converters.AddDateTimeTypeConverters("yyyy-MM-dd HH:mm:ss", true);//yyyy-MM-dd HH:mm:ss:fff
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // 忽略循环引用 仅.NET 6支持
                });

        services.AddControllersWithViews()
                //.AddAppLocalization()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    options.JsonSerializerOptions.Converters.AddDateTimeTypeConverters("yyyy-MM-dd HH:mm:ss", true);//yyyy-MM-dd HH:mm:ss:fff
                });

        services.AddMvc().AddNewtonsoftJson();
    }
}

/// <summary>
/// 规范化RESTful风格返回值
/// </summary>
[SuppressSniffer, UnifyModel(typeof(RestResult<>))]
public class RestResultProvider : IUnifyResultProvider
{
    public IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata)
    {
        // 解析异常信息
        var exceptionMetadata = UnifyContext.GetExceptionMetadata(context);

        return new JsonResult(new RestResult<object>
        {
            code = exceptionMetadata.StatusCode,
            Success = false,
            data = null,
            msg = exceptionMetadata.Errors,
            Extras = UnifyContext.Take(),
            Timestamp = DateTime.Now.Millisecond
        });
    }

    public IActionResult OnSucceeded(ActionExecutedContext context, object data)
    {
        switch (context.Result)
        {
            // 处理内容结果
            case ContentResult contentResult:
                data = contentResult.Content;
                break;
            // 处理对象结果
            case ObjectResult objectResult:
                data = objectResult.Value;
                break;
            case EmptyResult:
                data = null;
                break;
            default:
                return null;
        }

        return new JsonResult(new RestResult<object>
        {
            // code = context.Result is EmptyResult ? StatusCodes.Status204NoContent : StatusCodes.Status200OK,  // 处理没有返回值情况 204
            code = 200,
            Success = true,
            data = data,
            msg = "请求成功",
            Extras = UnifyContext.Take(),
            Timestamp = DateTime.Now.Millisecond
        });
    }

    public IActionResult OnValidateFailed(ActionExecutingContext context, ValidationMetadata metadata)
    {
        return new JsonResult(new RestResult<object>
        {
            code = StatusCodes.Status400BadRequest,
            Success = false,
            data = null,
            msg = metadata.ValidationResult,
            Extras = UnifyContext.Take(),
            Timestamp = DateTime.Now.Millisecond
        });
    }

    public async Task OnResponseStatusCodes(HttpContext context, int statusCode,
        UnifyResultSettingsOptions unifyResultSettings = null)
    {
        // 设置响应状态码
        UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);

        switch (statusCode)
        {
            // 处理 401 状态码
            case StatusCodes.Status401Unauthorized:
                await context.Response.WriteAsJsonAsync(new RestResult<object>
                {
                    code = StatusCodes.Status401Unauthorized,
                    Success = false,
                    data = null,
                    msg = "401 未经授权",
                    Extras = UnifyContext.Take(),
                    Timestamp = DateTime.Now.Millisecond
                });
                break;
            // 处理 403 状态码
            case StatusCodes.Status403Forbidden:
                await context.Response.WriteAsJsonAsync(new RestResult<object>
                {
                    code = StatusCodes.Status403Forbidden,
                    Success = false,
                    data = null,
                    msg = "403 禁止访问",
                    Extras = UnifyContext.Take(),
                    Timestamp = DateTime.Now.Millisecond
                });
                break;
            default: break;
        }
    }
}

/// <summary>
/// RESTful风格---XIAONUO返回格式
/// </summary>
/// <typeparam name="T"></typeparam>
[SuppressSniffer]
public class RestResult<T>
{
    /// <summary>
    /// 执行成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 状态码
    /// </summary>
    public int? code { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public object msg { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public T data { get; set; }

    /// <summary>
    /// 附加数据
    /// </summary>
    public object Extras { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }
}