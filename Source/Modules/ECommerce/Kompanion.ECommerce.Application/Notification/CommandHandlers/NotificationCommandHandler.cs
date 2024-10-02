using Kompanion.Application.Abstractions;
using Kompanion.Application.Extensions;
using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Notification.Commands;
using Microsoft.Extensions.Logging;

namespace Kompanion.ECommerce.Application.Notification.CommandHandlers;

public sealed class NotificationCommandHandler : ICommandHandler<SendEmailCommand, ApiResponse>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<NotificationCommandHandler> _logger;

    public NotificationCommandHandler(IEmailService emailService, ILogger<NotificationCommandHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _emailService.SendEmail(request.Email, cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Email gönderilemedi!");

            return new ApiResponse().BadRequest();
        }

        return new ApiResponse().Ok();
    }
}

