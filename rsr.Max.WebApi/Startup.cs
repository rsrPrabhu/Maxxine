using System.Configuration;
using Cortside.AspNetCore;
using Cortside.AspNetCore.AccessControl;
using Cortside.AspNetCore.ApplicationInsights;
using Cortside.AspNetCore.Auditable;
using Cortside.AspNetCore.Auditable.Entities;
using Cortside.AspNetCore.Builder;
using Cortside.AspNetCore.Swagger;
using Cortside.AspNetCore.EntityFramework;
using Cortside.Common.Messages.Filters;
using Cortside.DomainEvent;
using Cortside.DomainEvent.EntityFramework;
using Cortside.Health;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using rsr.Max.Bootstrap;
using rsr.Max.Data;
using rsr.Max.DomainEvent;
using rsr.Max.Health;
using rsr.Max.WebApi.Installers;

namespace rsr.Max.WebApi;

public class Startup 
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
         // setup global default json serializer settings
            JsonConvert.DefaultSettings = JsonNetUtility.GlobalDefaultSettings;

            // add ApplicationInsights telemetry
            var serviceName = Configuration["Service:Name"];
            // var config = Configuration.GetSection("ApplicationInsights").Get<ApplicationInsightsServiceOptions>();
            // services.AddApplicationInsights(serviceName, config);

            // add database context with interfaces
            services.AddDatabaseContext<IDatabaseContext, DatabaseContext>(Configuration);

            // add health services
            // services.AddHealth(o => {
            //     o.UseConfiguration(Configuration);
            //     o.AddCustomCheck("example", typeof(ExampleCheck));
            // });

            // add domain event receiver with handlers
            services.AddDomainEventReceiver(o => {
                o.UseConfiguration(Configuration);
                o.AddHandler<OrderStateChangedEvent, OrderStateChangedHandler>();
            });

            // add domain event publish with outbox
            services.AddDomainEventOutboxPublisher<DatabaseContext>(Configuration);

            // add controllers and all of the api defaults
            services.AddApiDefaults(InternalDateTimeHandling.Utc, options => {
                options.Filters.Add<MessageExceptionResponseFilter>();
            });

            // add SubjectPrincipal for auditing
            services.AddSubjectPrincipal();
            services.AddTransient<ISubjectFactory<Subject>, DefaultSubjectFactory>();

            // Add the access control using IdentityServer and PolicyServer
            services.AddAccessControl(Configuration);

            // Add swagger with versioning and OpenID Connect configuration using Newtonsoft
            //services.AddSwagger(Configuration, "Acme.ShoppingCart API", "Acme.ShoppingCart API", ["v1", "v2"]);

            // add service for handling encryption of search parameters
            services.AddEncryptionService(Configuration["Encryption:Secret"]);

            // setup and register boostrapper and it's installers
            services.AddBootStrapper<DefaultApplicationBootStrapper>(Configuration, o => {
                o.AddInstaller(new ModelMapperInstaller());
            });
        // add your services here
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Version", Version = "v1" });                
        });
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();
        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Max xin e V1");
        });
    }
}