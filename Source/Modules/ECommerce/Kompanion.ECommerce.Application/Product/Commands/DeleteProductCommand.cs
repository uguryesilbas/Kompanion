using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;

namespace Kompanion.ECommerce.Application.Product.Commands;

public sealed record DeleteProductCommand(int Id) : BaseCommand<ApiResponse>
{
}
