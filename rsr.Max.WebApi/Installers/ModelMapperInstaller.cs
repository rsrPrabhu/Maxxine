using Cortside.Common.BootStrap;
using rsr.Max.WebApi.Mappers;

namespace rsr.Max.WebApi.Installers;

public class ModelMapperInstaller : IInstaller {
    public void Install(IServiceCollection services, IConfiguration configuration) {
        services.AddSingletonClassesBySuffix<OrderModelMapper>("Mapper");
    }
}