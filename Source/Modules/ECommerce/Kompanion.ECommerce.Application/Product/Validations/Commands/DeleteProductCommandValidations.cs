using FluentValidation;
using Kompanion.ECommerce.Application.Product.Commands;

namespace Kompanion.ECommerce.Application.Product.Validations.Commands;

public sealed class DeleteProductCommandValidations : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidations()
    {
        RuleFor(x => x.Id)
         .GreaterThan(0)
         .WithMessage("Ürün Id bilgisi 0'dan büyük olmalıdır!");
    }
}

