using FluentValidation;
using Kompanion.ECommerce.Application.Product.Commands;

namespace Kompanion.ECommerce.Application.Product.Validations.Commands;

public class UpdateProductPriceCommandValidations : AbstractValidator<UpdateProductPriceCommand>
{
    public UpdateProductPriceCommandValidations()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id bilgisi 0'dan büyük olmalıdır!");

        RuleFor(x => x.CountryId)
          .GreaterThan(0)
          .WithMessage("Ülke Id bilgisi 0'dan büyük olmalıdır!");

        RuleFor(x => x.Price)
         .GreaterThan(0)
         .WithMessage("Fiyat bilgisi 0'dan büyük olmalıdır!");
    }
}

