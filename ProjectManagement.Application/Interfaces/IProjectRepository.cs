using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(Guid id);

        Task<IEnumerable<Project>> GetAllAsync();

        Task<IEnumerable<Project>> SearchAsync(string keyword);

        Task AddAsync(Project project);

        Task UpdateAsync(Project project);

        Task DeleteAsync(Guid id);
    }
}