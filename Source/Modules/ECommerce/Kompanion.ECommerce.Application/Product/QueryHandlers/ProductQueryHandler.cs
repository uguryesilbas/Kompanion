using Kompanion.Application.Extensions;
using Kompanion.Application.MediatR.Queries;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Product.Dtos;
using Kompanion.ECommerce.Application.Product.Mappings;
using Kompanion.ECommerce.Application.Product.Queries;
using Kompanion.ECommerce.Domain.Product;

namespace Kompanion.ECommerce.Application.Product.QueryHandlers;

public class ProductQueryHandler : IQueryHandler<GetProductQuery, ApiResponse<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public ProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ApiResponse<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        ProductEntity product = await _productRepository.FindByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            return new ApiResponse<ProductDto>(null).BadRequest().AddError("Ürün bulunamadı!");
        }

        return new ApiResponse<ProductDto>(product.MapToDto()).Ok();
    }
}

