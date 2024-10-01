using Kompanion.Domain.Abstracts;
using Kompanion.Domain.Extensions;
using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.Product;

public sealed partial class ProductPriceEntity : BaseEntity, ITrackableEntity
{
    public ProductPriceEntity()
    {

    }

    public ProductPriceEntity(int productId, int countryId, decimal price)
    {
        ProductId = productId;
        CountryId = countryId;
        Price = price;
        CreatedDateTime = DateTimeExtensions.Now;
    }

    public override int Id { get; set; }
    public int ProductId { get; private set; }
    public int CountryId { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }
    public int CreatedUserId { get; private set; }
    public int? UpdatedUserId { get; private set; }

    public static ProductPriceEntity CreateNew(int productId, int countryId, decimal price) => new(productId, countryId, price);
}

public sealed partial class ProductPriceEntity
{
    public void UpdateCountry(int countryId)
    {
        CountryId = countryId;
        UpdatedDateTime = DateTimeExtensions.Now;
    }

    public void UpdatePrice(decimal price)
    {
        Price = price;
        UpdatedDateTime = DateTimeExtensions.Now;
    }
}

