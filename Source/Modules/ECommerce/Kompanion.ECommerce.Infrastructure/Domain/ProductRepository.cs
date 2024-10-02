using Kompanion.Domain.Interfaces;
using Kompanion.ECommerce.Domain.Product;
using Kompanion.ECommerce.Infrastructure.Constants;
using Kompanion.ECommerce.Infrastructure.Context;

namespace Kompanion.ECommerce.Infrastructure.Domain;

internal sealed class ProductRepository : IProductRepository
{
    private readonly IECommerceDbContext _dbContext;
    public IPersistenceDbContext DbContext => _dbContext;

    public ProductRepository(IECommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> InsertAsync(ProductEntity entity, CancellationToken cancellationToken = default)
    {
        return await _dbContext.InsertAsync(StoreProcedureConstants.ProductsConstants.SaveOrUpdateStoreProcedureName, entity, cancellationToken);
    }

    public async Task<ProductEntity> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.FindByIdAsync<ProductEntity>(StoreProcedureConstants.ProductsConstants.FindByIdProcedureName, id, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ProductEntity entity, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UpdateAsync(StoreProcedureConstants.ProductsConstants.SaveOrUpdateStoreProcedureName, entity, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.DeleteAsync(StoreProcedureConstants.ProductsConstants.DeleteByIdProcedureName, id, cancellationToken);
    }
}
