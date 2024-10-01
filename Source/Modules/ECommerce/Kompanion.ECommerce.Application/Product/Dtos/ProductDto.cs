using Kompanion.ECommerce.Domain.Product;

namespace Kompanion.ECommerce.Application.Product.Dtos;

public class ProductDto
{
    public ProductDto()
    {
        
    }
    public ProductDto(int id, string productName, string description, int stockQuantity, DateTime createdDateTime, DateTime? updatedDateTime, int createdUserId, int? updatedUserId)
    {
        Id = id;
        ProductName = productName;
        Description = description;
        StockQuantity = stockQuantity;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        CreatedUserId = createdUserId;
        UpdatedUserId = updatedUserId;
    }

    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    public int CreatedUserId { get; set; }
    public int? UpdatedUserId { get; set; }

    public static explicit operator ProductDto(ProductEntity entity) => new(entity.Id, entity.ProductName, entity.Description, entity.StockQuantity, entity.CreatedDateTime, entity.UpdatedDateTime, entity.CreatedUserId, entity.UpdatedUserId);
}

