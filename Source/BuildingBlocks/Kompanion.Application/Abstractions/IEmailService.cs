namespace Kompanion.Application.Abstractions;

public interface IEmailService
{
    Task SendEmail(string email, CancellationToken cancellationToken = default);
}

