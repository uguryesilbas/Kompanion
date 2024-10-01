using Kompanion.Domain.Abstracts;
using Kompanion.Domain.Extensions;
using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.Product;

public sealed partial class ProductEntity : BaseEntity, ITrackableEntity
{
    public ProductEntity()
    {

    }

    private ProductEntity(string productName, string description, int stockQuantity)
    {
        ProductName = productName;
        Description = description;
        StockQuantity = stockQuantity;
        CreatedDateTime = DateTimeExtensions.Now;
    }

    public override int Id { get; set; }
    public string ProductName { get; private set; }
    public string Description { get; private set; }
    public int StockQuantity { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }
    public int CreatedUserId { get; private set; }
    public int? UpdatedUserId { get; private set; }

    public static ProductEntity CreateNew(string productName, string description, int stockQuantity) => new(productName, description, stockQuantity);
}
public sealed partial class ProductEntity
{
    public void UpdateProductName(string productName)
    {
        ProductName = productName;
        UpdatedDateTime = DateTimeExtensions.Now;
    }

    public void UpdateDescription(string description)
    {
        Description = description;
        UpdatedDateTime = DateTimeExtensions.Now;
    }

    public void UpdateStockQuantity(int stockQuantity)
    {
        StockQuantity = stockQuantity;
        UpdatedDateTime = DateTimeExtensions.Now;
    }
}
