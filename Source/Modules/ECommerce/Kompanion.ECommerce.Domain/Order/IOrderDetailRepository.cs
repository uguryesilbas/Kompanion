using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.Order;

public interface IOrderDetailRepository : IRepository<OrderDetailEntity>
{
    Task<List<OrderDetailEntity>> GetOrderDetailsByOrderId(int orderId, CancellationToken cancellationToken = default);
}

