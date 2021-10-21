using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NeighborsNetwork.Shared.DbHelpers;
using NeighborsNetwork.Users.Db;
using NeighborsNetwork.Users.Service.BackgroundServices;
using NeighborsNetwork.Users.Service.Processing;

namespace NeighborsNetwork.Users.Service
{
    public class UserServiceConfiguration
    {
        public string? ConnectionString { get; set; }
        public bool IsDevelopment { get; set; }
    }

    public static class ServicesConfigurationExtensions
    {
        public static void AddUserService(this IServiceCollection services, Action<UserServiceConfiguration>? applyConfigurationFn = default)
        {
            var config = new UserServiceConfiguration();
            applyConfigurationFn ??= DefaultConfiguration;
            applyConfigurationFn(config);

            services.AddSeeders();
            services.AddExternalPackages();
            services.AddUsersDb(config.ConnectionString, config.IsDevelopment);

            services.AddHostedService<InitializationBackgroundService>();
        }

        private static void DefaultConfiguration(UserServiceConfiguration cfg)
        {
            cfg.ConnectionString = default;
            cfg.IsDevelopment = true;
        }

        private static void AddExternalPackages(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IUsersServiceIdentifier));
            services.AddMediatR(typeof(IUserProcessingServiceIdentifier));
        }

        private static void AddSeeders(this IServiceCollection services)
        {
            services.AddSeeders<UsersContext>();
        }

        private static void AddUsersDb(this IServiceCollection services, string? connectionString, bool isDev)
        {
            services.AddDbContext<UsersContext>(options =>
            {
                if (isDev || string.IsNullOrWhiteSpace(connectionString))
                {
                    options.UseInMemoryDatabase("users-service");
                }
                else
                {
                    options.UseNpgsql(
                        connectionString,
                        builder =>
                        {
                            builder.MigrationsAssembly(typeof(IUsersServiceIdentifier).Assembly.FullName);
                        });
                }
            });
        }
    }
}
