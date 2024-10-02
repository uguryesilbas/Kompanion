using Kompanion.Domain.Interfaces;
using Kompanion.ECommerce.Domain.Product;
using Kompanion.ECommerce.Infrastructure.Constants;
using Kompanion.ECommerce.Infrastructure.Context;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Reflection;

namespace Kompanion.ECommerce.Infrastructure.Domain;

internal sealed class ProductPriceRepository : IProductPriceRepository
{
    private readonly IECommerceDbContext _dbContext;

    public IPersistenceDbContext DbContext => _dbContext;

    public ProductPriceRepository(IECommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> InsertAsync(ProductPriceEntity entity, CancellationToken cancellationToken = default)
    {
        return await _dbContext.InsertAsync(StoreProcedureConstants.ProductPriceConstants.SaveOrUpdateProductPrice, entity, cancellationToken);
    }

    public async Task<bool> UpdateAsync(ProductPriceEntity entity, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UpdateAsync(StoreProcedureConstants.ProductPriceConstants.SaveOrUpdateProductPrice, entity, cancellationToken);
    }

    public async Task<ProductPriceEntity> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.FindByIdAsync<ProductPriceEntity>(StoreProcedureConstants.ProductPriceConstants.GetProductPriceById, id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.DeleteAsync(StoreProcedureConstants.ProductPriceConstants.DeleteProductPriceById, id, cancellationToken);
    }
}

