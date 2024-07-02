using MediatR;

namespace ProjectManagement.Application.Commands.Projects
{
    public class DeleteProjectCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}