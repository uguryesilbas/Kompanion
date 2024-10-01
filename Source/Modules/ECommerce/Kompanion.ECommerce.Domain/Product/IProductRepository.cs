using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.Product;

public interface IProductRepository : IRepository<ProductEntity>
{
    Task<ProductEntity> FindByIdAsync(int id, CancellationToken cancellationToken = default);
}
