using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NeighborsNetwork.Shared.DbHelpers
{
    public interface ISeeder<in T> where T : DbContext
    {
        Task SeedAsync(T ctx, CancellationToken token = default);
    }
}
