using Kompanion.Application.Extensions;
using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Product.Commands;
using Kompanion.ECommerce.Domain.Product;

namespace Kompanion.ECommerce.Application.Product.CommandHandlers;

public sealed class ProductPriceCommandHandler : ICommandHandler<CreateProductPriceCommand, ApiResponse<int>>, ICommandHandler<UpdateProductPriceCommand, ApiResponse>, ICommandHandler<DeleteProductPriceCommand, ApiResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductPriceRepository _productPriceRepository;

    public ProductPriceCommandHandler(IProductRepository productRepository, IProductPriceRepository productPriceRepository)
    {
        _productRepository = productRepository;
        _productPriceRepository = productPriceRepository;
    }

    public async Task<ApiResponse<int>> Handle(CreateProductPriceCommand request, CancellationToken cancellationToken)
    {
        ProductEntity product = await _productRepository.FindByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            return new ApiResponse<int>(0).NotFound().AddError("Ürün bulunamadı!");
        }

        //TODO: Ürün için yukarıda yapılan kontrolün aynısı ülke içinde yapılabilir. Vakit kalırsa geri dönülecek.
        //TODO: Ürüne ve aynı ülkeye ait fiyat bulunuyorsa işlem durdurulabilir.

        ProductPriceEntity newProductPrice = ProductPriceEntity.CreateNew(request.ProductId, request.CountryId, request.Price);

        bool result = await _productPriceRepository.InsertAsync(newProductPrice, cancellationToken);

        return result
            ? new ApiResponse<int>(newProductPrice.Id).Created()
            : new ApiResponse<int>(0).BadRequest().AddError("Fiyat oluşturulamadı!");

    }

    public async Task<ApiResponse> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        ProductPriceEntity productPrice = await _productPriceRepository.FindByIdAsync(request.Id, cancellationToken);

        if (productPrice is null)
        {
            return new ApiResponse().NotFound().AddError("Ürün fiyatı bulunamadı!");
        }

        //TODO Ülke kontrolü yapılabilir.
        //TODO Fiyata ait ürün satışı yapılmışsa işlem durdurulabilir.

        productPrice.UpdateCountry(request.CountryId);

        productPrice.UpdatePrice(request.Price);

        bool result = await _productPriceRepository.UpdateAsync(productPrice, cancellationToken);

        return result
            ? new ApiResponse().Ok()
            : new ApiResponse().BadRequest().AddError("Fiyat güncellenemedi!");
    }

    public async Task<ApiResponse> Handle(DeleteProductPriceCommand request, CancellationToken cancellationToken)
    {
        ProductPriceEntity productPrice = await _productPriceRepository.FindByIdAsync(request.Id, cancellationToken);

        if (productPrice is null)
        {
            return new ApiResponse().NotFound().AddError("Ürün fiyatı bulunamadı!");
        }

        //TODO Fiyata ait ürün satışı yapılmışsa işlem durdurulabilir.

        bool result = await _productPriceRepository.DeleteAsync(request.Id, cancellationToken);

        return result
        ? new ApiResponse().Ok()
        : new ApiResponse().BadRequest().AddError("Fiyat silinemedi!");
    }
}

