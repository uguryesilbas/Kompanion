using Kompanion.Application.Extensions;
using Kompanion.Domain.Abstracts;
using Kompanion.Domain.Interfaces;
using Kompanion.ECommerce.Domain.Order;
using Kompanion.ECommerce.Domain.Order.Enums;
using Kompanion.ECommerce.Infrastructure.Constants;
using Kompanion.ECommerce.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System.Data;
using static Kompanion.ECommerce.Infrastructure.Constants.StoreProcedureConstants;

namespace Kompanion.ECommerce.Infrastructure.Domain;

internal class OrderRepository : IOrderRepository
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

    public async Task<OrderEntity> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.FindByIdAsync<OrderEntity>(StoreProcedureConstants.Order.GetOrderById, id, cancellationToken);
    }

    public async Task<OrderEntity> CreateOrder(int countryId, List<OrderDetailEntity> orderDetails, CancellationToken cancellationToken = default)
    {
        int userId = _contextAccessor.GetUserId();

        OrderEntity order = OrderEntity.CreateNew(OrderStatusType.Suspend, countryId);

        List<MySqlParameter> orderParameters = new()
        {
            new MySqlParameter("p_Status",MySqlDbType.Int16)  {Value = OrderStatusType.Suspend },
            new MySqlParameter("p_CountryId",MySqlDbType.UInt16)  {Value = countryId },
            new MySqlParameter("p_CreatedUserId",MySqlDbType.Int32)  {Value = userId }
        };

        MySqlParameter outputIdParam = new("p_LastInsertID", MySqlDbType.Int32)
        {
            Direction = ParameterDirection.Output,
        };

        try
        {

            MySqlConnection connection = await _dbContext.GetConnectionAsync(cancellationToken);

            using MySqlTransaction transaction = await connection.BeginTransactionAsync(cancellationToken);

            try
            {
                using MySqlCommand orderCommand = new(StoreProcedureConstants.Order.InsertOrder, connection, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };

                orderCommand.Parameters.AddRange(orderParameters.ToArray());

                orderCommand.Parameters.Add(outputIdParam);

                int result = await orderCommand.ExecuteNonQueryAsync(cancellationToken);

                if (result <= 0)
                {
                    throw new Exception("Sipariş oluşturulamadı!");
                }

                typeof(OrderEntity).GetProperty(nameof(BaseEntity.Id)).SetValue(order, (int)outputIdParam.Value, null);


                //MySQL de Dictionary mantığında çalışan bir şey bulamadığımdan dolayı loop ile yapıldı. 
                foreach (OrderDetailEntity orderDetail in orderDetails)
                {
                    List<MySqlParameter> parameters = new()
                    {
                        new MySqlParameter("p_ProductId",MySqlDbType.Int32)  {Value = orderDetail.ProductId },
                        new MySqlParameter("p_Quantity",MySqlDbType.Int32)  {Value = orderDetail.Quantity },
                        new MySqlParameter("p_OrderId",MySqlDbType.Int32)  {Value = order.Id },
                        new MySqlParameter("p_UserId",MySqlDbType.UInt16)  {Value = userId },
                        new MySqlParameter("p_CountryId",MySqlDbType.UInt16)  {Value = countryId }
                    };

                    using MySqlCommand command = new(StoreProcedureConstants.Order.CreateOrder, connection, transaction)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddRange(parameters.ToArray());

                    await command.ExecuteNonQueryAsync(cancellationToken);
                }
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            await transaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await _dbContext.DisposeAsync();
        }

        return order;
    }

    public async Task UpdateStatusAsync(int orderId, OrderStatusType orderStatus, CancellationToken cancellationToken = default)
    {
        List<MySqlParameter> parameters = new()
        {
            new MySqlParameter("p_Id",MySqlDbType.Int32)  {Value = orderId },
            new MySqlParameter("p_Status",MySqlDbType.Int16)  {Value = (int)orderStatus }
        };

        await _dbContext.ExecuteStoredProcedureAsync(StoreProcedureConstants.Order.UpdateOrderStatusById, cancellationToken, parameters.ToArray());
    }
}

