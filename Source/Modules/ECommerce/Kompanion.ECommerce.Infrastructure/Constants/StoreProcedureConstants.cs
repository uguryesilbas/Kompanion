namespace Kompanion.ECommerce.Infrastructure.Constants;

public static class StoreProcedureConstants
{
    public static class ProductsConstants
    {
        public const string SaveOrUpdateStoreProcedureName = "SaveOrUpdateProduct";
        public const string FindByIdProcedureName = "GetProductById";
        public const string DeleteByIdProcedureName = "DeleteProductById";
    }

    public static class ProductPriceConstants
    {
        public const string SaveOrUpdateProductPrice = nameof(SaveOrUpdateProductPrice);
        public const string GetProductPriceById = nameof(GetProductPriceById);
        public const string DeleteProductPriceById = nameof(DeleteProductPriceById);
    }

    public static class BankConstants
    {
        public const string GetBankByIdStoreProcedureName = "GetBankById";
    }
}
