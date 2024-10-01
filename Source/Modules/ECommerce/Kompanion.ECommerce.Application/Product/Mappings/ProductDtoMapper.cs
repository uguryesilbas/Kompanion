using Kompanion.ECommerce.Application.Product.Dtos;
using Kompanion.ECommerce.Domain.Product;

namespace Kompanion.ECommerce.Application.Product.Mappings;

public static class ProductDtoMapper
{
    public static ProductDto MapToDto(this ProductEntity entity) => (ProductDto)entity;
    public static List<ProductDto> MapToDto(this IEnumerable<ProductEntity> source) => source.Select(x => x.MapToDto()).ToList();
}
