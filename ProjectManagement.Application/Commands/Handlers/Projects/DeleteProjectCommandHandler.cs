using MediatR;

using ProjectManagement.Application.Commands.Projects;
using ProjectManagement.Application.Interfaces;

namespace ProjectManagement.Application.Commands.Handlers.Projects
{
    public class DeleteProjectCommandHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            await _projectRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}