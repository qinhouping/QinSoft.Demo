namespace QinSoft.Demo.Furion
{
    [DependsOn(typeof(DALServiceComponent))]
    public class BLLServiceComponent : IServiceComponent
    {
        public void Load(IServiceCollection services, ComponentContext componentContext)
        {
            services.AddServices();
            services.AddMappers();
            services.AddEventBus();
        }
    }
}
