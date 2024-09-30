using Microsoft.EntityFrameworkCore;
using Kompanion.Infrastructure.Extensions;

namespace Kompanion.Infrastructure.Database.Abstracts;

public abstract class BaseDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyAllConfigurations();
    }
}