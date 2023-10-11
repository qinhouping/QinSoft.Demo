namespace QinSoft.Demo.Furion
{
    public class ApiServiceComponent : IWebComponent
    {
        public void Load(WebApplicationBuilder builder, ComponentContext componentContext)
        {
            builder.Services.AddFileConfiger(options =>
            {
                options.ExpireIn = 600;
            });
            builder.Services.AddDatabaseManager(options =>
            {

            });
        }
    }
}
