using System.Configuration;
using Cortside.AspNetCore;
using Cortside.AspNetCore.AccessControl;
using Cortside.AspNetCore.ApplicationInsights;
using Cortside.AspNetCore.Builder;
using Cortside.AspNetCore.Swagger;
using Cortside.Common.Messages.Filters;
using Cortside.AspNetCore.EntityFramework;
using Cortside.Health;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using rsr.Max.Data;

namespace rsr.Max.WebApi;

public class Startup : IWebApiStartup
{
    /// <summary>
    /// Startup
    /// </summary>
    /// <param name="configuration"></param>
    [ActivatorUtilitiesConstructor]
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    /// <summary>
    /// Startup
    /// </summary>
    public Startup() {
    }
    
    
    /// <summary>
    /// Config
    /// </summary>
    private IConfiguration Configuration { get; set; }

    /// <summary>
    /// Sets the Configuration to be used
    /// </summary>
    /// <param name="config"></param>
    public void UseConfiguration(IConfiguration config) {
        Configuration = config;
    }
    
    /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services) {
            // setup global default json serializer settings
            JsonConvert.DefaultSettings = JsonNetUtility.GlobalDefaultSettings;

            // add ApplicationInsights telemetry
            var serviceName = Configuration["Service:Name"];
            var config = Configuration.GetSection("ApplicationInsights").Get<ApplicationInsightsServiceOptions>();
            services.AddApplicationInsights(serviceName, config);

            // add database context with interfaces
             services.AddDatabaseContext<IDatabaseContext, DatabaseContext>(Configuration);

            // add health services
            services.AddHealth(o => {
                o.UseConfiguration(Configuration);
                //nnn o.AddCustomCheck("example", typeof(ExampleCheck));
            });

            // add domain event receiver with handlers
            //nnn// services.AddDomainEventReceiver(o => {
            //nnn//     o.UseConfiguration(Configuration);
            //nnn//     o.AddHandler<OrderStateChangedEvent, OrderStateChangedHandler>();
            //nnn });

            // add domain event publish with outbox
            //nnn services.AddDomainEventOutboxPublisher<DatabaseContext>(Configuration);

            // add controllers and all of the api defaults
            services.AddApiDefaults(InternalDateTimeHandling.Utc, options => {
                options.Filters.Add<MessageExceptionResponseFilter>();
            });

            // add SubjectPrincipal for auditing
            //nn services.AddSubjectPrincipal();
            //nn services.AddTransient<ISubjectFactory<Subject>, DefaultSubjectFactory>();

            // Add the access control using IdentityServer and PolicyServer
            services.AddAccessControl(Configuration);

            // Add swagger with versioning and OpenID Connect configuration using Newtonsoft
            //nnn services.AddSwagger(Configuration, "Acme.ShoppingCart API", "Acme.ShoppingCart API", ["v1", "v2"]);

            // add service for handling encryption of search parameters
            services.AddEncryptionService(Configuration["Encryption:Secret"]);

            // setup and register boostrapper and it's installers
            //nnn services.AddBootStrapper<DefaultApplicationBootStrapper>(Configuration, o => {
            //nnn   o.AddInstaller(new ModelMapperInstaller());
            //nnn });
        }
    
    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="provider"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider) {
        // Can be used for more analytic information if not using an APM of some kind.
        // Need to add installer MiniProfilerInstaller to default bootstrapper or as an installer above
        // uncomment: app.UseMiniProfiler()

        app.UseApiDefaults(Configuration);
        app.UseSwagger("Acme.ShoppingCart Api", provider);

        // order of the following matters
        app.UseAuthentication();
        //nnn app.UseSubjectPrincipal(); // intentionally set after UseAuthentication
        app.UseRouting();
        app.UseAuthorization(); // intentionally set after UseRouting
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}