using FluentValidation;
using Kompanion.ECommerce.Application.Bank.Queries;

namespace Kompanion.ECommerce.Application.Bank.Validations.Queries;

public class GetBankQueryValidations : AbstractValidator<GetBankQuery>
{
    public GetBankQueryValidations()
    {
        RuleFor(x => x.BankId)
            .GreaterThan(0)
            .WithMessage("Banka Id bilgisi 0'dan büyük olmalıdır!");
    }
}
