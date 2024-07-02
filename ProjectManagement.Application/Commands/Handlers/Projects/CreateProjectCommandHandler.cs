using MediatR;

using ProjectManagement.Application.Commands.Projects;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Commands.Handlers.Projects
{
    public class CreateProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProjectCommand, Guid>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.GroupId, request.ProjectNumber, request.Name!, request.Customer!, request.Status!, request.StartDate)
            {
                EndDate = request.EndDate
            };

            await _projectRepository.AddAsync(project);

            return project.Id;
        }
    }
}