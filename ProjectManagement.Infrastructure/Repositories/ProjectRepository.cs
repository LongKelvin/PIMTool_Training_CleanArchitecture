using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Infrastructure.Data.DataContext;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class ProjectRepository(PIMToolDbContext context) : Repository<Project>(context), IProjectRepository
    {
    }
}