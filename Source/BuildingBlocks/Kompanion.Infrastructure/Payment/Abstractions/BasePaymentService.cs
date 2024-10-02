namespace Kompanion.Infrastructure.Payment.Abstractions;

public abstract class BasePaymentService
{
    public virtual Task ReceivePaymentAsync(int cardNumber, string expiryDate, string cvv, decimal price, CancellationToken cancellationToken = default)
    {
        try
        {
            throw new Exception("Ödeme hatası");
            // Ödeme ile ilgili gerekli geliştirmeler burada yapılır.
            return Task.CompletedTask;
        }
        catch
        {
            throw;
        }
    }

    public virtual Task RefundPaymentAsync(int cardNumber, string expiryDate, string cvv, decimal price, CancellationToken cancellationToken = default)
    {
        try
        {
            // İade ile ilgili gerekli geliştirmeler burada yapılır.
            return Task.CompletedTask;
        }
        catch
        {
            throw;
        }
    }
}

