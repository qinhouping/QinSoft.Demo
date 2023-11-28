using Microsoft.OpenApi.Models;
using QinSoft.Demo.Api;
using QinSoft.Demo.Api.Filter;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddXmlDataContractSerializerFormatters().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new NullableDateTimeJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new NullableDateTimeOffsetJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new NullableTimeSpanJsonConverter());
});
// Add filters to the container
builder.Services.AddMvc(options =>
{
    options.Filters.Add<TestActionFilterAttribute>();
    options.Filters.Add<TestExceptionFilterAttribute>();
});
// Add authentication to the container
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "test";
    options.AddScheme<TestAuthenticationHandler>("test", "Test");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("test", builder => builder.RequireAuthenticatedUser());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("default", new OpenApiInfo()
    {
        Title = "QinSoft.Demo",
        Description = "QinSoft.Demo Default Api",
        Version = "1.0.0"
    });

    options.SwaggerDoc("extension", new OpenApiInfo()
    {
        Title = "QinSoft.Demo",
        Description = "QinSoft.Demo Extension Api",
        Version = "1.0.0"
    });

    options.SwaggerDoc("temporary", new OpenApiInfo()
    {
        Title = "QinSoft.Demo",
        Description = "QinSoft.Demo Temporary Api",
        Version = "1.0.0"
    });

    // 为 Swagger JSON and UI设置xml文档注释路径
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "QinSoft.Demo.Api.xml"));

    //添加自定义header授权参数
    options.OperationFilter<SwaggerConfig>(Array.Empty<object>());
});

builder.Services.AddFileConfiger(options =>
{
    options.ExpireIn = 600;
});
builder.Services.AddDatabaseManager(options =>
{

});

builder.Services.AddServices();
builder.Services.AddMappers();
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("default/swagger.json", "default");
        options.SwaggerEndpoint("extension/swagger.json", "extension");
        options.SwaggerEndpoint("temporary/swagger.json", "temporary");
    });
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();