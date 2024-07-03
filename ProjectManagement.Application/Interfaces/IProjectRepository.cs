using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(Guid id);

        Task<IEnumerable<Project>> GetAllAsync(bool eagerLoading = false);

        Task<IEnumerable<Project>> SearchAsync(string keyword);

        Task AddAsync(Project project);

        Task UpdateAsync(Project project);

        Task DeleteAsync(Guid id);
    }
}