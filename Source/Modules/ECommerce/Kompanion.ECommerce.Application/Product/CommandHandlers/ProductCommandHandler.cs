using Kompanion.Application.Extensions;
using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Product.Commands;
using Kompanion.ECommerce.Domain.Product;

namespace Kompanion.ECommerce.Application.Product.CommandHandlers;

public sealed class ProductCommandHandler : ICommandHandler<CreateProductCommand, ApiResponse<int>>, ICommandHandler<UpdateProductCommand, ApiResponse>, ICommandHandler<DeleteProductCommand, ApiResponse>
{
    private readonly IProductRepository _productRepository;

    public ProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ApiResponse<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductEntity newProduct = ProductEntity.CreateNew(request.ProductName, request.Description, request.StockQuantity);

        bool result = await _productRepository.InsertAsync(newProduct, cancellationToken);

        return result
            ? new ApiResponse<int>(newProduct.Id).Created()
            : new ApiResponse<int>(0).BadRequest().AddError("Ürün oluşturulamadı!");
    }

    public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        ProductEntity product = await _productRepository.FindByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return new ApiResponse().NotFound().AddError("Ürün bulunamadı!");
        }

        product.UpdateProductName(request.ProductName);

        product.UpdateDescription(request.Description);

        product.UpdateStockQuantity(request.StockQuantity);

        bool result = await _productRepository.UpdateAsync(product, cancellationToken);

        return result
            ? new ApiResponse().Ok()
            : new ApiResponse().BadRequest().AddError("Ürün güncellenemedi!");
    }

    public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        ProductEntity product = await _productRepository.FindByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return new ApiResponse().NotFound().AddError("Ürün bulunamadı!");
        }

        bool result = await _productRepository.DeleteAsync(product.Id, cancellationToken);

        return result
         ? new ApiResponse().Ok()
         : new ApiResponse().BadRequest().AddError("Ürün silinemedi!");
    }
}
