using MediatR;

using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Queries.Projects
{
    public class GetAllProjectQuery : IRequest<IEnumerable<Project>>
    {
        public Func<IQueryable<Project>, IQueryable<Project>>? Includes { get; set; } = null;
    }
}