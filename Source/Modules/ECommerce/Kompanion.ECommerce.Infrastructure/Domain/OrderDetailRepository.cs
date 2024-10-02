using Elastic.Clients.Elasticsearch;
using Kompanion.ECommerce.Domain.Order;
using Kompanion.ECommerce.Infrastructure.Constants;
using Kompanion.ECommerce.Infrastructure.Context;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Reflection;

namespace Kompanion.ECommerce.Infrastructure.Domain;

internal class OrderDetailRepository : IOrderDetailRepository
{
    private readonly IECommerceDbContext _dbContext;

    public OrderDetailRepository(IECommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertAsync(OrderDetailEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(OrderDetailEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<OrderDetailEntity>> GetOrderDetailsByOrderId(int orderId, CancellationToken cancellationToken = default)
    {
        try
        {
            List<OrderDetailEntity> orderDetails = new();

            MySqlConnection connection = await _dbContext.GetConnectionAsync(cancellationToken);

            using MySqlCommand command = _dbContext.CreateStoredProcedureCommand(StoreProcedureConstants.OrderDetail.GetOrderDetailsByOrderId, connection);

            command.Parameters.AddWithValue("p_Id", orderId);

            using (DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken))
            {
                if (!reader.HasRows)
                    return orderDetails;

                while (await reader.ReadAsync(cancellationToken))
                {
                    OrderDetailEntity entity = new();

                    foreach (PropertyInfo prop in typeof(OrderDetailEntity).GetProperties())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                        {
                            prop.SetValue(entity, reader[prop.Name]);
                        }
                    }

                    orderDetails.Add(entity);
                }
            }

            return orderDetails;
        }
        finally
        {
            await _dbContext.DisposeAsync();
        }
    }
}

