using Basic.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Basic.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        //private readonly IUser _user;
        //private readonly TimeProvider _dateTime;

        public AuditableEntityInterceptor(
            // IUser user,
            TimeProvider dateTime)
        {
            // _user = user;
            // _dateTime = dateTime;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
                {

                    if (entry.State == EntityState.Added)
                    {
                        //TODO: Add UserId
                        //entry.Entity.CreatedBy = _user.Id;
                        entry.Entity.CreatedById = "1";
                        entry.Entity.CreatedByName = "test";
                        entry.Entity.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        //TODO: Add UserId in case update
                        entry.Entity.LastModifiedByName = "test";
                        entry.Entity.LastModified = DateTime.Now;
                    }
                }
            }
        }
    }

    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
