using MediatR;

using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Queries.Projects
{
    public class GetProjectByIdQuery : IRequest<Project>
    {
        public Guid Id { get; set; }

        public Func<IQueryable<Project>, IQueryable<Project>>? Includes { get; set; } = null;
    }
}