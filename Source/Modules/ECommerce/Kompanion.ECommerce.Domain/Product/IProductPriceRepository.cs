using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.Product;

public interface IProductPriceRepository : IRepository<ProductPriceEntity>
{
    Task<ProductPriceEntity> FindByIdAsync(int id, CancellationToken cancellationToken = default);
}

