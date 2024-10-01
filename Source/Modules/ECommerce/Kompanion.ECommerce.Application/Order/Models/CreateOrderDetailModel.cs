namespace Kompanion.ECommerce.Application.Order.Models;

public record CreateOrderDetailModel
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}

