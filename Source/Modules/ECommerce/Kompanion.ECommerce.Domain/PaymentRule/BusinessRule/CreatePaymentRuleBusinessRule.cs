using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.PaymentRule.BusinessRule;

public class CreatePaymentRuleBusinessRule : IBusinessRule
{
    public CreatePaymentRuleBusinessRule(decimal? minAmount, decimal? maxAmount, DateTime? startDateTime, DateTime? endDateTime)
    {
        MinAmount = minAmount;
        MaxAmount = maxAmount;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }

    public decimal? MinAmount { get; private set; }
    public decimal? MaxAmount { get; private set; }
    public DateTime? StartDateTime { get; private set; }
    public DateTime? EndDateTime { get; private set; }

    public string Message => "Ödeme kuralı kaydetmek için en az bir şart girilmesi gerekmektedir." ;

    public bool IsBroken(CancellationToken cancellationToken = default)
    {
        return !MinAmount.HasValue 
            && !MaxAmount.HasValue 
            && !StartDateTime.HasValue 
            && !EndDateTime.HasValue;
    }
}

