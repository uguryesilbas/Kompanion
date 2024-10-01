using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;

namespace Kompanion.ECommerce.Application.Product.Commands;

public sealed record CreateProductCommand : BaseCommand<ApiResponse<int>>
{
    public string ProductName { get; init; }
    public string Description { get; init; }
    public int StockQuantity { get; init; }
}
