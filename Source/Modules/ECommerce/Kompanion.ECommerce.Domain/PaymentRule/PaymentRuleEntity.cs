using Kompanion.Domain.Abstracts;
using Kompanion.Domain.Interfaces;
using Kompanion.ECommerce.Domain.PaymentRule.BusinessRule;

namespace Kompanion.ECommerce.Domain.PaymentRule;

public sealed partial class PaymentRuleEntity : BaseEntity, ITrackableEntity
{
    public PaymentRuleEntity()
    {

    }

    private PaymentRuleEntity(int productId, int bankId, int countryId, decimal? minAmount, decimal? maxAmount, DateTime? startDateTime, DateTime? endDateTime)
    {
        MinAmount = minAmount;
        MaxAmount = maxAmount;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        ProductId = productId;
        BankId = bankId;
        CountryId = countryId;

        CheckRule(new CreatePaymentRuleBusinessRule(MinAmount, MaxAmount, StartDateTime, EndDateTime));
    }

    public override int Id { get; set; }
    public decimal? MinAmount { get; private set; }
    public decimal? MaxAmount { get; private set; }
    public DateTime? StartDateTime { get; private set; }
    public DateTime? EndDateTime { get; private set; }
    public int ProductId { get; private set; }
    public int BankId { get; private set; }
    public int CountryId { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }
    public int CreatedUserId { get; private set; }
    public int? UpdatedUserId { get; private set; }

    public static PaymentRuleEntity CreateNew(int productId, int bankId, int countryId, decimal? minAmount = null, decimal? maxAmount = null, DateTime? startDateTime = null, DateTime? endDateTime = null)
    {
        return new PaymentRuleEntity(productId, bankId, countryId, minAmount, maxAmount, startDateTime, endDateTime);
    }
}
