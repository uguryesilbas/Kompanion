using Kompanion.Domain.Abstracts;

namespace Kompanion.Domain.Interfaces
{
    public interface IRepository
    {
        IPersistenceDbContext DbContext { get; }
    }

    public interface IRepository<in TEntity> : IRepository where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
