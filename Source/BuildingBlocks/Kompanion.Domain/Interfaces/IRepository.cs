using Kompanion.Domain.Abstracts;

namespace Kompanion.Domain.Interfaces;

public interface IRepository
{
}

public interface IRepository<in TEntity> : IRepository where TEntity : BaseEntity
{
    Task<bool> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

