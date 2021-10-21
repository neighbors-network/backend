using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NeighborsNetwork.Shared.SwaggerHelpers;
using NeighborsNetwork.Users.Service;

namespace NeighborsNetwork.Server
{
    public class Startup
    {
        private readonly Assembly[] _servicesAssemblies =
        {
            typeof(IUsersServiceIdentifier).Assembly,
        };

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        private IConfiguration Configuration { get; }
        private IHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddUserService(cfg =>
            {
                cfg.ConnectionString = Configuration.GetConnectionString("Users");
            });

            services.AddMvcCore().AddApiExplorer();

            services.AddRouting(options => options.LowercaseUrls = true);

            var mvcBuilder = services.AddControllers();
            foreach (var assembly in _servicesAssemblies)
            {
                mvcBuilder.AddApplicationPart(assembly);
            }

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("server", new OpenApiInfo { Title = "NeighborsNetwork.Server", Version = "v1" });
                c.AddServicesSwaggerGenOptions(_servicesAssemblies);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (Env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint("/swagger/server/swagger.json", "NeighborsNetwork.Server v1");
                        c.RegisterServicesEndpoints(_servicesAssemblies);
                    }
                );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSwagger();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}
