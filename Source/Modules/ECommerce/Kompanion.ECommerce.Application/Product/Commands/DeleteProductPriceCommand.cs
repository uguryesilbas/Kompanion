using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;

namespace Kompanion.ECommerce.Application.Product.Commands;

public sealed record DeleteProductPriceCommand(int Id) : BaseCommand<ApiResponse>;

