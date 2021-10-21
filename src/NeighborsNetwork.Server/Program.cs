using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace NeighborsNetwork.Server
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
                 .Build()
                 .RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>();
                builder.UseKestrel(opts =>
                {
                    opts.Limits.MaxConcurrentConnections = 5000;
                    opts.Limits.MaxConcurrentUpgradedConnections = 5000;
                });
            });
        }
    }
}
