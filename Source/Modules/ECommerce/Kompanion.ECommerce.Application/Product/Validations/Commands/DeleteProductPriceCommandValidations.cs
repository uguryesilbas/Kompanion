using FluentValidation;
using Kompanion.ECommerce.Application.Product.Commands;

namespace Kompanion.ECommerce.Application.Product.Validations.Commands;

public class DeleteProductPriceCommandValidations : AbstractValidator<DeleteProductPriceCommand>
{
    public DeleteProductPriceCommandValidations()
    {
        RuleFor(x => x.Id)
        .GreaterThan(0)
        .WithMessage("Id bilgisi 0'dan büyük olmalıdır!");
    }
}

