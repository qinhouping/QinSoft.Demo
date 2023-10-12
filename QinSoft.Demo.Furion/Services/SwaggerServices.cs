using Furion.ConfigurableOptions;
using Furion.SpecificationDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QinSoft.Demo.Furion.Services
{
    [ApiDescriptionSettings("Swagger")]
    [DynamicApiController]
    [AllowAnonymous]
    public class SwaggerServices
    {
        [HttpPost, NonUnify]
        public int CheckUrl()
        {
            return 401;
        }

        [HttpPost, NonUnify]
        public int SubmitUrl([FromForm] SpecificationAuth auth)
        {
            // 读取配置信息
            var loginInfo = App.GetOptions<SwaggerLoginOptions>();

            if (auth.UserName == loginInfo.UserName && auth.Password == loginInfo.Password)
            {
                return 200;
            }
            else
            {
                return 401;
            }
        }
    }

    [OptionsSettings("SpecificationDocumentSettings:LoginInfo")]
    public class SwaggerLoginOptions : IConfigurableOptions
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
