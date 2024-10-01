using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using System.Text.Json.Serialization;

namespace Kompanion.ECommerce.Application.Product.Commands;

public sealed record UpdateProductPriceCommand : BaseCommand<ApiResponse>
{
    [JsonIgnore]
    public int Id { get; init; }
    public int CountryId { get; init; }
    public decimal Price { get; init; }
}

