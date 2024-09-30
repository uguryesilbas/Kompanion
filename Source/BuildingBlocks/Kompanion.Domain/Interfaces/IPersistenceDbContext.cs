namespace Kompanion.Domain.Interfaces;

public interface IPersistenceDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}