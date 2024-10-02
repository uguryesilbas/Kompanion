using Kompanion.Domain.Interfaces;
using Kompanion.ECommerce.Domain.Order.Enums;

namespace Kompanion.ECommerce.Domain.Order;

public interface IOrderRepository : IRepository<OrderEntity>
{
    Task<OrderEntity> CreateOrder(int countryId, List<OrderDetailEntity> orderDetails, CancellationToken cancellationToken = default);
    Task<OrderEntity> FindByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateStatusAsync(int orderId, OrderStatusType orderStatus, CancellationToken cancellationToken = default);
}

