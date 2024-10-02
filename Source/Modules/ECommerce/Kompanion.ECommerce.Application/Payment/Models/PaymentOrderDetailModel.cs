using Kompanion.Application.Enums;

namespace Kompanion.ECommerce.Application.Payment.Models;

public record PaymentOrderDetailModel
{
    public PaymentBankType Bank { get; init; }

    public decimal TotalAmount { get; init; }
}

