namespace QinSoft.Demo.Furion
{
    public class DALServiceComponent : IServiceComponent
    {
        public void Load(IServiceCollection services, ComponentContext componentContext)
        {
            services.AddRepositories();
        }
    }
}
