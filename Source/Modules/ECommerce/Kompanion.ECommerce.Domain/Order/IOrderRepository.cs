using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.Order;

public interface IOrderRepository : IRepository<OrderEntity>
{
    Task CreateOrder(int countryId, OrderDetailEntity orderDetail, CancellationToken cancellationToken = default);
}

