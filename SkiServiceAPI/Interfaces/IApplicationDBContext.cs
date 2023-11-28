using Microsoft.EntityFrameworkCore;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Priority> Priorities { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<State> States { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Support for generic repositories
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task MigrateAsync();
    }
}