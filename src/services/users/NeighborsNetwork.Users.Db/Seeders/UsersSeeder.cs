using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NeighborsNetwork.Shared.DbHelpers;
using NeighborsNetwork.Users.Db.Entities;

namespace NeighborsNetwork.Users.Db.Seeders
{
    [UsedImplicitly]
    public class UsersSeeder : ISeeder<UsersContext>
    {
        public async Task SeedAsync(UsersContext ctx, CancellationToken token = default)
        {
            if (!await ctx.Users.AnyAsync(token))
            {
                await ctx.Users.AddRangeAsync(new []
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test",
                        Surname = "Test",
                        Middlename = null,
                        DateOfBirth = new DateOnly(1990, 10, 10),
                        Disabled = false,
                        CreatedAt = DateTimeOffset.Now
                    }
                });
            }

            await ctx.SaveChangesAsync(token);
        }
    }
}
