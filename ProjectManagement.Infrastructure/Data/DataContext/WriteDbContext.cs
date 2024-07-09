using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Infrastructure.Data.DataContext
{
    public class WriteDbContext(DbContextOptions<WriteDbContext> dbOptions) : BaseDbContext<WriteDbContext>(dbOptions)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}