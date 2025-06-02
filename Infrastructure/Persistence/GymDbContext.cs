using Domain.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace Persistence
{
    public class GymDbContext:IdentityDbContext<AppUser>
    {
        public GymDbContext(DbContextOptions<GymDbContext>options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            //foreach (var entityType in builder.Model.GetEntityTypes())
            //{
            //    // Only apply this rule to entities that have IsDeleted
            //    if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
            //    {
            //        var parameter = Expression.Parameter(entityType.ClrType, "e");               // e =>
            //        var isDeletedProperty = Expression.Property(parameter, nameof(EntityBase.IsDeleted));         // e.IsDeleted
            //        var notDeleted = Expression.Not(isDeletedProperty);                          // !e.IsDeleted
            //        var lambda = Expression.Lambda(notDeleted, parameter);                       // e => !e.IsDeleted

            //        builder.Entity(entityType.ClrType).HasQueryFilter(lambda);              // Apply the filter
            //    }
            //}

            base.OnModelCreating(builder);
        }
    }
}
