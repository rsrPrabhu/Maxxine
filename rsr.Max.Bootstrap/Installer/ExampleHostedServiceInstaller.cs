using Cortside.Common.BootStrap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rsr.Max.Configuration;
using rsr.Max.Hosting;

namespace rsr.Max.Bootstrap.Installer;

public class ExampleHostedServiceInstaller : IInstaller {
    public void Install(IServiceCollection services, IConfiguration configuration) {
        services.AddSingleton(configuration.GetSection("ExampleHostedService").Get<ExampleHostedServiceConfiguration>());
        services.AddHostedService<ExampleHostedService>();
    }
}