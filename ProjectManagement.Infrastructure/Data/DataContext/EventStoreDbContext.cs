using Microsoft.EntityFrameworkCore;

using ProjectManagement.Infrastructure.Data.Mappings;

using ProjectManagerment.Core.SharedKernel;

namespace ProjectManagement.Infrastructure.Data.DataContext
{
    public class EventStoreDbContext(DbContextOptions<EventStoreDbContext> dbOptions)
        : BaseDbContext<EventStoreDbContext>(dbOptions)
    {
        public DbSet<EventStore> EventStores => Set<EventStore>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EventStoreConfiguration());
        }
    }
}
