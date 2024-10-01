using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using System.Text.Json.Serialization;

namespace Kompanion.ECommerce.Application.Product.Commands;

public sealed record UpdateProductCommand : BaseCommand<ApiResponse>
{
    [JsonIgnore]
    public int Id { get; init; }
    public string ProductName { get; init; }
    public string Description { get; init; }
    public int StockQuantity { get; init; }
}

