using Kompanion.Infrastructure.Invoicing.Abstractions;

namespace Kompanion.Infrastructure.Invoicing.Services;

public sealed class InvoiceService : IInvoiceService
{
    public Task CancelInvoice(CancellationToken cancellationToken = default)
    {
        try
        {
            //Fatura oluşturma işlemini yapar...
            return Task.CompletedTask;
        }
        catch
        {

            throw;
        }
    }

    public Task CreateInvoice(CancellationToken cancellationToken = default)
    {
        try
        {
            //Fatura iptal işlemini yapar...
            return Task.CompletedTask;
        }
        catch
        {

            throw;
        }
    }
}
