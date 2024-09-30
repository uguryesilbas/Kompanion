using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Kompanion.Application.Extensions;
using Kompanion.Domain.Extensions;
using Kompanion.Domain.Interfaces;

namespace Kompanion.Infrastructure.Database.Interceptors;

internal class TrackableEntityInterceptor(IHttpContextAccessor contextAccessor) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext context)
    {
        List<EntityEntry> entityEntries = context?.ChangeTracker.Entries()
            .Where(x => x.Entity is ITrackableEntity && (x.State is EntityState.Added or EntityState.Modified))
            .ToList();

        if (entityEntries is not { Count: > 0 })
        {
            return;
        }

        Guid userId = contextAccessor.GetUserId();

        entityEntries.ForEach(entity =>
        {
            switch (entity.State)
            {
                case EntityState.Modified:
                    {
                        entity.Property(nameof(ITrackableEntity.UpdatedDateTime)).CurrentValue = DateTimeExtensions.Now;
                        
                        entity.Property(nameof(ITrackableEntity.UpdatedUserId)).CurrentValue = userId;

                        break;
                    }
                case EntityState.Added:
                    {
                        entity.Property(nameof(ITrackableEntity.CreatedDateTime)).CurrentValue = DateTimeExtensions.Now;

                        entity.Property(nameof(ITrackableEntity.CreatedUserId)).CurrentValue = userId;

                        break;
                    }
            }
        });
    }
}