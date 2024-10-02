namespace Kompanion.ECommerce.Infrastructure.Constants;

public static class StoreProcedureConstants
{
    public static class ProductsConstants
    {
        public const string SaveOrUpdateStoreProcedureName = "SaveOrUpdateProduct";
        public const string FindByIdProcedureName = "GetProductById";
        public const string DeleteByIdProcedureName = "DeleteProductById";
        public const string StockControlByProductId = nameof(StockControlByProductId);
    }

    public static class ProductPriceConstants
    {
        public const string SaveOrUpdateProductPrice = nameof(SaveOrUpdateProductPrice);
        public const string GetProductPriceById = nameof(GetProductPriceById);
        public const string DeleteProductPriceById = nameof(DeleteProductPriceById);
        public const string IsExistsPriceByProductIdWithCountryId = nameof(IsExistsPriceByProductIdWithCountryId);
        public const string GetPricesByProductIdWithCountryId = nameof(GetPricesByProductIdWithCountryId);
    }

    public static class Order
    {
        public const string InsertOrder = nameof(InsertOrder);
        public const string CreateOrder = nameof(CreateOrder);
        public const string GetOrderById = nameof(GetOrderById);
        public const string UpdateOrderStatusById = nameof(UpdateOrderStatusById);
    }

    public static class OrderDetail
    {
        public const string GetOrderDetailsByOrderId = nameof(GetOrderDetailsByOrderId);
    }

    public static class PaymentRule
    {
        public const string GetPaymentRulesByProductIds = nameof(GetPaymentRulesByProductIds);
    }
}
