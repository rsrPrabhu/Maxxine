using System.Threading.Tasks;
using Cortside.AspNetCore.Builder;
using rsr.Max.WebApi;
using Serilog;

namespace rsr.Max.WebApi {
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseWebRoot("wwwroot");
                });
    }
    
    
}