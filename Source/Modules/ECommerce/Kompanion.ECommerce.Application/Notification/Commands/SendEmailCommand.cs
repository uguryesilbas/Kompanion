using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;

namespace Kompanion.ECommerce.Application.Notification.Commands;

public sealed record SendEmailCommand : BaseCommand<ApiResponse>
{
    public string Email { get; init; }
}

