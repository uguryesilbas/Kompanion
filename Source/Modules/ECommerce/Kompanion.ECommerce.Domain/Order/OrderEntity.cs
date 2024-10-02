using Kompanion.Domain.Abstracts;
using Kompanion.Domain.Extensions;
using Kompanion.Domain.Interfaces;
using Kompanion.ECommerce.Domain.Order.Enums;

namespace Kompanion.ECommerce.Domain.Order;

public sealed partial class OrderEntity : BaseEntity, ITrackableEntity
{
    public OrderEntity()
    {

    }

    private OrderEntity(int status, int countryId)
    {
        Status = status;
        CountryId = countryId;
        CreatedDateTime = DateTimeExtensions.Now;
    }

    public override int Id { get; set; }
    public int Status { get; private set; }
    public int CountryId { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }
    public int CreatedUserId { get; private set; }
    public int? UpdatedUserId { get; private set; }

    public static OrderEntity CreateNew(OrderStatusType status, int countryId) => new((int)status, countryId);

}

public sealed partial class OrderEntity
{
    public void UpdateStatus(OrderStatusType orderStatus)
    {
        Status = (int)orderStatus;
        UpdatedDateTime = DateTimeExtensions.Now;
    }
}

