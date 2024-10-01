using Kompanion.Domain.Abstracts;
using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.Product;

public sealed class ProductEntity : BaseEntity, ITrackableEntity
{
    public ProductEntity()
    {

    }

    public ProductEntity(string productName, string description, int stockQuantity)
    {
        ProductName = productName;
        Description = description;
        StockQuantity = stockQuantity;
    }

    public override int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    public int CreatedUserId { get; set; }
    public int? UpdatedUserId { get; set; }
}

