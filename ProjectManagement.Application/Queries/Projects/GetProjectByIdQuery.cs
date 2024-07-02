using MediatR;

using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Queries.Projects
{
    public class GetProjectByIdQuery : IRequest<Project>
    {
        public Guid Id { get; set; }
    }
}