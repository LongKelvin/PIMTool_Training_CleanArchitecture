using Microsoft.EntityFrameworkCore;

using ProjectManagement.Infrastructure.Data.PersistenceModels;

namespace ProjectManagement.Infrastructure.Data.DataContext
{
    public class PIMToolDbContext(DbContextOptions<PIMToolDbContext> options) : DbContext(options)
    {
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectEntity>()
                .HasMany(p => p.Employees)
                .WithMany(e => e.Projects)
                .UsingEntity<Dictionary<string, object>>(
                    "ProjectEmployees",
                    j => j.HasOne<EmployeeEntity>()
                          .WithMany()
                          .HasForeignKey("EmployeeId")
                          .OnDelete(DeleteBehavior.Restrict),
                    j => j.HasOne<ProjectEntity>()
                          .WithMany()
                          .HasForeignKey("ProjectId")
                          .OnDelete(DeleteBehavior.Restrict)
                );

            modelBuilder.Entity<GroupEntity>()
                .HasOne(g => g.GroupLeader)
                .WithMany()
                .HasForeignKey(g => g.GroupLeaderId);
        }
    }
}
