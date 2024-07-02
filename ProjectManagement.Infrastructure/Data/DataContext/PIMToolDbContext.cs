using Microsoft.EntityFrameworkCore;

using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Infrastructure.Data.DataContext
{
    public class PIMToolDbContext(DbContextOptions<PIMToolDbContext> options) : DbContext(options)
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Domain.Entities.Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Group)
                .WithMany(g => g.Projects)
                .HasForeignKey(p => p.GroupId);

            modelBuilder.Entity<ProjectEmployee>()
                .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Employee)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Group>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.GroupLeader)
                .WithMany()
                .HasForeignKey(g => g.GroupLeaderId);
        }
    }
}