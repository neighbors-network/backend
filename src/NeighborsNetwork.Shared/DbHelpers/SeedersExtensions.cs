using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NeighborsNetwork.Shared.DbHelpers
{
    public static class SeedersExtensions
    {
        public static void AddSeeders<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            var types = GetSeedersInAsm<TContext>(typeof(TContext).Assembly);

            foreach (var type in types)
            {
                services.AddSingleton(typeof(ISeeder<TContext>), type);
            }
        }

        private static IEnumerable<Type> GetSeedersInAsm<TContext>(Assembly asm) where TContext : DbContext
        {
            return asm.GetTypes().Where(type => type.IsClass && !type.IsAbstract && type.IsAssignableTo(typeof(ISeeder<TContext>)));
        }
    }
}
