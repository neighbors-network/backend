using Microsoft.EntityFrameworkCore;
using NeighborsNetwork.Users.Db.Entities;

namespace NeighborsNetwork.Users.Db
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersContext).Assembly);
        }
    }
}
