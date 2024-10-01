using FluentValidation;
using Kompanion.ECommerce.Application.Product.Commands;

namespace Kompanion.ECommerce.Application.Product.Validations.Commands;

public sealed class UpdateProductCommandValidations : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidations()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Ürün Id bilgisi 0'dan büyük olmalıdır!");

        RuleFor(x => x.ProductName)
          .Must(x => !string.IsNullOrWhiteSpace(x))
          .WithMessage("Ürün adı boş olamaz!")
          .MaximumLength(255)
          .WithMessage("Ürün adı maximum 255 karakter olmalıdır!");

        RuleFor(x => x.Description)
            .MaximumLength(250)
            .WithMessage("Ürün açıklaması maximum 250 karakter olmalıdır!");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Ürün stok bilgisi 0 veya 0'dan büyük olmalıdır!");
    }
}
