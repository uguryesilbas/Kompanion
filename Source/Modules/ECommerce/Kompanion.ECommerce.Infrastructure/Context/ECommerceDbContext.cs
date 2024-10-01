using Kompanion.Infrastructure.Database.Abstracts;

namespace Kompanion.ECommerce.Infrastructure.Context;

public class ECommerceDbContext : BaseDbContext, IECommerceDbContext
{
    public ECommerceDbContext(IServiceProvider serviceProvider, string connectionStringSectionName) : base(serviceProvider, connectionStringSectionName)
    {

    }
}

