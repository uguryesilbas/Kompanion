namespace Kompanion.Infrastructure.Invoicing.Abstractions;

public interface IInvoiceService
{
    Task CreateInvoice(CancellationToken cancellationToken = default);

    Task CancelInvoice(CancellationToken cancellationToken = default);
}
