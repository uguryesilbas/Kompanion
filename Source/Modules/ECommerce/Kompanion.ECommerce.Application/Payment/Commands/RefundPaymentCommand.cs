using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Payment.Models;

namespace Kompanion.ECommerce.Application.Payment.Commands;

public sealed record RefundPaymentCommand : BaseCommand<ApiResponse>
{
    //kart bilgileri kullanıcıdan alınır.
    // public CardInfo CreditCardInfo { get; set; }

    public List<PaymentOrderDetailModel> OrderDetails { get; init; } = new();
}

