using Kompanion.ECommerce.Domain.Order;
using Kompanion.ECommerce.Domain.PaymentRule;
using Kompanion.ECommerce.Infrastructure.Constants;
using Kompanion.ECommerce.Infrastructure.Context;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kompanion.ECommerce.Infrastructure.Domain;

internal sealed class PaymentRuleRepository : IPaymentRuleRepository
{
    private readonly IECommerceDbContext _dbContext;

    public PaymentRuleRepository(IECommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertAsync(PaymentRuleEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(PaymentRuleEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PaymentRuleEntity>> GetPaymentRulesByProductIdsAsync(List<int> productIds, int countryId, CancellationToken cancellationToken = default)
    {
        try
        {
            List<PaymentRuleEntity> paymentRules = new();

            MySqlConnection connection = await _dbContext.GetConnectionAsync(cancellationToken);

            using MySqlCommand command = _dbContext.CreateStoredProcedureCommand(StoreProcedureConstants.PaymentRule.GetPaymentRulesByProductIds, connection);

            command.Parameters.AddWithValue("p_Ids", productIds);

            command.Parameters.AddWithValue("p_CountryId", countryId);

            using (DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken))
            {
                if (!reader.HasRows)
                    return paymentRules;

                while (await reader.ReadAsync(cancellationToken))
                {
                    PaymentRuleEntity entity = new();

                    foreach (PropertyInfo prop in typeof(PaymentRuleEntity).GetProperties())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal(prop.Name)))
                        {
                            prop.SetValue(entity, reader[prop.Name]);
                        }
                    }

                    paymentRules.Add(entity);
                }
            }

            return paymentRules;
        }
        finally
        {
            await _dbContext.DisposeAsync();
        }
    }
}

