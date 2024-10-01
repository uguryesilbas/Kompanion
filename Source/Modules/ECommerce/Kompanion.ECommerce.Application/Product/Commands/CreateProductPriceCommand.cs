using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;

namespace Kompanion.ECommerce.Application.Product.Commands;

public sealed record CreateProductPriceCommand : BaseCommand<ApiResponse<int>>
{
    public int ProductId { get; init; }
    public int CountryId { get; init; }
    public decimal Price { get; init; }
}

