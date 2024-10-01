using Kompanion.Application.Caching;
using Kompanion.Application.MediatR.Queries;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Constants;
using Kompanion.ECommerce.Application.Product.Dtos;

namespace Kompanion.ECommerce.Application.Product.Queries;

public sealed record GetProductQuery(int Id) : BaseQuery<ApiResponse<ProductDto>>, ICacheRequest
{
    public string CacheKey => $"{CacheKeyConstants.ProductCacheKeyConstants.GetProduct}:{Id}";
    public int? Database => 0;
    public TimeSpan? CacheExpiry => TimeSpan.FromMinutes(5);
}

