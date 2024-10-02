using Kompanion.ECommerce.Domain.Order;

namespace Kompanion.ECommerce.Infrastructure.Saga.Order;

public interface IOrderProcessSagaService
{
    Task ProcessOrderAsync(OrderEntity order, CancellationToken cancellationToken = default);
}
