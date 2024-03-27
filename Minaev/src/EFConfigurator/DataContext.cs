using EFConfigurator.Models;
using Microsoft.EntityFrameworkCore;

namespace EFConfigurator;

public class DataContext : DbContext
{
    public DbSet<AccountDB> Accounts { get; set; }
    public DbSet<JwtTokenDB> BannedJwtTokens { get; set; }
    public DbSet<TransportDB> Transports { get; set; }
    public DbSet<RentDB> Rents { get; set; }

    public DataContext() { }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Model.GetEntityTypes()
            .SelectMany(entity => entity.GetProperties())
            .ToList()
            .ForEach(property => property.SetColumnName(property.Name.ToLower()));
    }

    public async Task<Int32> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    public DbSet<T> DbSet<T>() where T : class
    {
        return Set<T>();
    }

    public IQueryable<T> Query<T>() where T : class
    {
        return Set<T>();
    }
}
