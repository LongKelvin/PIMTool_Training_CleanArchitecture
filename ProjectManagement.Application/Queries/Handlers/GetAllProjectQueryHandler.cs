using MediatR;

using ProjectManagement.Application.Interfaces;

using ProjectManagement.Application.Queries.Projects;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Queries.Handlers
{
    public class GetAllProjectQueryHandler(IProjectRepository projectRepository) : IRequestHandler<GetAllProjectQuery, IEnumerable<Project>>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<IEnumerable<Project>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetAllAsync(cancellationToken: cancellationToken);
        }
    }
}