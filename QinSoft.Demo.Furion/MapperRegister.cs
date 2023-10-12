using Mapster;
using QinSoft.Demo.Furion.Services;

namespace QinSoft.Demo.Furion
{
    public class MapperRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<TestOptions, OutResult>().Map(dest => dest.AA, src => src.A);
        }
    }
}
