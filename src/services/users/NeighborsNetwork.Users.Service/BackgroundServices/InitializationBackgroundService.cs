using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeighborsNetwork.Shared.DbHelpers;
using NeighborsNetwork.Users.Db;

namespace NeighborsNetwork.Users.Service.BackgroundServices
{
    public class InitializationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<ISeeder<UsersContext>> _seeders;

        public InitializationBackgroundService(IServiceProvider serviceProvider, IEnumerable<ISeeder<UsersContext>> seeders)
        {
            _serviceProvider = serviceProvider;
            _seeders = seeders;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();

            var ctx = scope.ServiceProvider.GetRequiredService<UsersContext>();

            foreach (var seeder in _seeders)
            {
                await seeder.SeedAsync(ctx, stoppingToken);
            }
        }
    }
}
