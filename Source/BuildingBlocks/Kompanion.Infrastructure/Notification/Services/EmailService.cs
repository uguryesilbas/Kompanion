using Kompanion.Application.Abstractions;

namespace Kompanion.Infrastructure.Notification.Services;

public sealed class EmailService : IEmailService
{
    public Task SendEmail(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            //Email gönderme işlemini yapar..
            return Task.CompletedTask;
        }
        catch
        {

            throw;
        }
    }
}

