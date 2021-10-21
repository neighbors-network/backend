using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace NeighborsNetwork.Users.Service
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            // builder.Services.AddUserService(builder.Configuration.GetConnectionString("Users"));
            services.AddUserService();
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("users", new OpenApiInfo { Title = "NeighborsNetwork.Users.Service", Version = "v1" });
            });

            var app = builder.Build();

            app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/users/swagger.json", "NeighborsNetwork.Users.Service v1");
                }
            );

            app.Run();
        }
    }
}
