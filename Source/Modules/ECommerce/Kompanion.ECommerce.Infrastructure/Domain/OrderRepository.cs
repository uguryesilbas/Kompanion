using Kompanion.Application.Extensions;
using Kompanion.Domain.Interfaces;
using Kompanion.ECommerce.Domain.Order;
using Kompanion.ECommerce.Domain.Order.Enums;
using Kompanion.ECommerce.Infrastructure.Constants;
using Kompanion.ECommerce.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

namespace Kompanion.ECommerce.Infrastructure.Domain;

public class OrderRepository : IOrderRepository
{
    private readonly IECommerceDbContext _dbContext;
    private readonly IHttpContextAccessor _contextAccessor;

    public OrderRepository(IECommerceDbContext dbContext, IHttpContextAccessor contextAccessor)
    {
        _dbContext = dbContext;
        _contextAccessor = contextAccessor;
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertAsync(OrderEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(OrderEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task CreateOrder(int countryId, OrderDetailEntity orderDetail, CancellationToken cancellationToken = default)
    {
        List<MySqlParameter> parameters = new()
        {
            new MySqlParameter("p_ProductId",MySqlDbType.Int32)  {Value = orderDetail.ProductId },
            new MySqlParameter("p_Quantity",MySqlDbType.Int32)  {Value = orderDetail.Quantity },
            new MySqlParameter("p_OrderStatus",MySqlDbType.UInt16)  {Value = OrderStatusType.Suspend },
            new MySqlParameter("p_UserId",MySqlDbType.UInt16)  {Value = _contextAccessor.GetUserId() },
            new MySqlParameter("p_CountryId",MySqlDbType.UInt16)  {Value = countryId }
        };

        await _dbContext.ExecuteStoredProcedureAsync(StoreProcedureConstants.Order.CreateOrder, cancellationToken, parameters.ToArray());
    }
}

