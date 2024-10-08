﻿using Kompanion.Domain.Abstracts;
using Kompanion.Domain.Extensions;
using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.Order;

public sealed partial class OrderDetailEntity : BaseEntity, ITrackableEntity
{
    public OrderDetailEntity()
    {

    }

    private OrderDetailEntity(int orderId, int productId, decimal priceAtPurchase, int quantity, string currencyCodeAtPurchase)
    {
        OrderId = orderId;
        ProductId = productId;
        PriceAtPurchase = priceAtPurchase;
        Quantity = quantity;
        CurrencyCodeAtPurchase = currencyCodeAtPurchase;
        CreatedDateTime = DateTimeExtensions.Now;
    }

    public override int Id { get; set; }
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public decimal PriceAtPurchase { get; private set; }
    public int Quantity { get; private set; }
    public string CurrencyCodeAtPurchase { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime? UpdatedDateTime { get; private set; }
    public int CreatedUserId { get; private set; }
    public int? UpdatedUserId { get; private set; }

    public static OrderDetailEntity CreateNew(int orderId, int productId, decimal priceAtPurchase, int quantity, string currencyCodeAtPurchase) => new(orderId, productId, priceAtPurchase, quantity, currencyCodeAtPurchase);
}

public sealed partial class OrderDetailEntity
{

}
