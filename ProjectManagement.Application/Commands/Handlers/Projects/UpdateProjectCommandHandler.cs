using MediatR;

using ProjectManagement.Application.Commands.Projects;
using ProjectManagement.Application.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Exceptions;

namespace ProjectManagement.Application.Commands.Handlers.Projects
{
    public class UpdateProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project is null)
            {
                throw new NotFoundException(nameof(Project), request.Id);
            }

            project.Name = request.Name!;
            project.Customer = request.Customer!;
            project.Status = request.Status!;
            project.StartDate = request.StartDate;
            project.EndDate = request.EndDate;

            await _projectRepository.UpdateAsync(project);

            return Unit.Value;
        }
    }
}