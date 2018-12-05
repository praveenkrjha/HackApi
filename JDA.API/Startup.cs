using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacSerilogIntegration;
using JDA.API.Helpers;
using JDA.API.Middleware;
using JDA.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Converters;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;

namespace JDA.API
{
    /// <summary>
    /// JDA.API.Startup class for startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Add(new LegacyConfigurationProvider(env))
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            //Register Serilog and read configuration from appsettings.json
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        /// <summary>
        /// Gets the application container.
        /// </summary>
        /// <value>
        /// The application container.
        /// </value>
        public IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter
                {
                    CamelCaseText = true
                });
            });
            services.AddCors();
            // Adds services required for using options.
            services.AddOptions();


            // Register the IConfiguration instance which MyOptions binds against.
            services.Configure<ConnectionStrings>(Configuration.GetSection("connectionStrings"));
            services.Configure<AppSettings>(Configuration.GetSection("appSettings"));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "JDA Hack API", Version = "v1.0.2", TermsOfService = "All rights reserved byJDA", });

                c.IncludeXmlComments(System.IO.Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "JDA.API.xml"));
            });

            // Create the container builder.
            var builder = new ContainerBuilder();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.Load(new AssemblyName("JDA.BusinessLayer"))).Where(t => t.Name.EndsWith("Manager")).AsImplementedInterfaces().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.Load(new AssemblyName("JDA.Repository"))).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.Load(new AssemblyName("JDA.Common"))).Where(t => t.Name.EndsWith("Utility")).AsImplementedInterfaces().SingleInstance();

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Authorize", policy => policy.Requirements.Add(new TokenAuthorization()));
            });

            //Register logger
            builder.RegisterLogger();

            //services.AddTransient<IUserRepository, UserRepository>();
            builder.Populate(services);
            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="appLifetime">The application lifetime.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            app.UseSecurityMiddleware();
           // app.UseRequestResponseLogging();
            app.UseCors(builder =>
               builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
           );
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1.0.2");
            });

            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }
}
