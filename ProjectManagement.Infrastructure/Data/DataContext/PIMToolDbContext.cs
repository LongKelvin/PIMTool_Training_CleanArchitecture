using Microsoft.EntityFrameworkCore;

using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Infrastructure.Data.DataContext
{
    public class PIMToolDbContext(DbContextOptions<PIMToolDbContext> options) : DbContext(options)
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Domain.Entities.Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.GroupLeader)
                .WithOne()
                .HasForeignKey<Group>(g => g.GroupLeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Projects)
                .WithOne(p => p.Group)
                .HasForeignKey(p => p.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
             .HasMany(p => p.Employees)
             .WithMany(e => e.Projects)
             .UsingEntity<Dictionary<string, object>>(
                 "ProjectEmployee",
                 j => j.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId"),
                 j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId"));

            modelBuilder.Entity<Project>()
                .Property(p => p.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Timestamp)
                .IsRowVersion();
        }
    }
}