using Kompanion.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompanion.ECommerce.Domain.PaymentRule;

public interface IPaymentRuleRepository : IRepository<PaymentRuleEntity>
{
    Task<List<PaymentRuleEntity>> GetPaymentRulesByProductIdsAsync(List<int> productIds, int countryId, CancellationToken cancellationToken = default);
}

