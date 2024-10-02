using System.Threading;

namespace Kompanion.Application.Abstractions;

public interface IPaymentService
{
    Task ReceivePaymentAsync(int cardNumber, string expiryDate, string cvv, decimal price, CancellationToken cancellationToken = default);

    Task RefundPaymentAsync(int cardNumber, string expiryDate, string cvv, decimal price, CancellationToken cancellationToken = default);
}

