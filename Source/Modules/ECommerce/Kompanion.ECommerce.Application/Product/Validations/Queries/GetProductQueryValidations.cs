using FluentValidation;
using Kompanion.ECommerce.Application.Product.Queries;

namespace Kompanion.ECommerce.Application.Product.Validations.Queries;

public sealed class GetProductQueryValidations : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidations()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Ürün Id bilgisi 0'dan büyük olmalıdır!");
    }
}

