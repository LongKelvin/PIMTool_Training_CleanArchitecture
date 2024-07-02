using Microsoft.EntityFrameworkCore;

using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Infrastructure.Data.DataContext;

namespace ProjectManagement.Infrastructure.Repositories
{
    public class ProjectRepository(PIMToolDbContext context) : IProjectRepository
    {
        private readonly PIMToolDbContext _context = context;

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await _context.Projects.Include(p => p.ProjectEmployees)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Project>> SearchAsync(string keyword)
        {
            return await _context.Projects
                .Include(p => p.ProjectEmployees)
                .Where(p => p.Name.Contains(keyword) || p.Customer.Contains(keyword))
                .ToListAsync();
        }

        public async Task AddAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project!);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.ProjectEmployees)
                .ToListAsync();
        }
    }
}