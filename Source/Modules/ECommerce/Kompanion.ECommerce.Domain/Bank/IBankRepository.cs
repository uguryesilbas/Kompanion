using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.Bank;

public interface IBankRepository : IRepository<BankEntity>
{
    Task<BankEntity> FindByIdAsync(int bankId, CancellationToken cancellationToken = default);
}

