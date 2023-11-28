using Microsoft.OpenApi.Models;
using Nest;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace QinSoft.Demo.Api
{
    public class SwaggerConfig : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Token",
                Description = "Token",
                In = ParameterLocation.Header,
                Required = false
            });
        }
    }
}
