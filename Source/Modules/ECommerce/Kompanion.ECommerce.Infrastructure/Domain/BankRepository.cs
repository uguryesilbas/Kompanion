using Kompanion.Domain.Interfaces;
using Kompanion.ECommerce.Domain.Bank;
using Kompanion.ECommerce.Infrastructure.Context;
using MySql.Data.MySqlClient;

namespace Kompanion.ECommerce.Infrastructure.Domain;

public class BankRepository : IBankRepository
{
    private readonly IECommerceDbContext _dbContext;
    public IPersistenceDbContext DbContext => _dbContext;

    public BankRepository(IECommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BankEntity> FindByIdAsync(int bankId, CancellationToken cancellationToken = default)
    {
        const string StoreProcedureName = "GetBankById";
        const string ParameterName = "p_BankId";

        MySqlParameter parameter = new MySqlParameter(ParameterName, MySqlDbType.Binary) { Value = bankId };

        return await _dbContext.FindByIdAsync<BankEntity>(StoreProcedureName, parameter, cancellationToken);
    }

    public async Task<bool> InsertAsync(BankEntity entity, CancellationToken cancellationToken = default)
    {
        const string StoreProcedureName = "InsertBank";

        return await _dbContext.InsertAsync(StoreProcedureName, entity, cancellationToken);
    }
}

