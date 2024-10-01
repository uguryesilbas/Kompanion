using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Order.Models;

namespace Kompanion.ECommerce.Application.Order.Commands;

public sealed record CreateOrderCommand : BaseCommand<ApiResponse>
{
    public int CountryId { get; init; }
    public List<CreateOrderDetailModel> Orders { get; set; }
}

