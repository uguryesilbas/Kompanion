using FluentValidation;
using Kompanion.ECommerce.Application.Bank.Commands;

namespace Kompanion.ECommerce.Application.Bank.Validations.Commands;

public sealed class CreateBankCommandValidations : AbstractValidator<CreateBankCommand>
{
    public CreateBankCommandValidations()
    {
        RuleFor(x => x.BankName)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Banka adı zorunludur!")
            .MaximumLength(255)
            .WithMessage("Banka adının maximum uzunluğu 255 karakter olmalıdır!");

    }
}
