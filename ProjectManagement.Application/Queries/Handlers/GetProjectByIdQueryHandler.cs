using MediatR;

using ProjectManagement.Application.Interfaces;
using ProjectManagement.Application.Queries.Projects;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Queries.Handlers
{
    public class GetProjectByIdQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetProjectByIdQuery, Project>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Project?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetByIdAsync(request.Id);
        }
    }
}