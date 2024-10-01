using Kompanion.Domain.Interfaces;
using Kompanion.ECommerce.Domain.Bank;
using Kompanion.ECommerce.Infrastructure.Constants;
using Kompanion.ECommerce.Infrastructure.Context;
using MySql.Data.MySqlClient;

namespace Kompanion.ECommerce.Infrastructure.Domain;

public sealed class BankRepository : IBankRepository
{
    private readonly IECommerceDbContext _dbContext;
    public IPersistenceDbContext DbContext => _dbContext;

    public BankRepository(IECommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BankEntity> FindByIdAsync(int bankId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.FindByIdAsync<BankEntity>(StoreProcedureConstants.BankConstants.GetBankByIdStoreProcedureName, bankId, cancellationToken);
    }

    public async Task<bool> InsertAsync(BankEntity entity, CancellationToken cancellationToken = default)
    {
        const string StoreProcedureName = "InsertBank";

        return await _dbContext.InsertAsync(StoreProcedureName, entity, cancellationToken);
    }

    public Task<bool> UpdateAsync(BankEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

