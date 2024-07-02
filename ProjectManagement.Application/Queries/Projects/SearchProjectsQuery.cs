using MediatR;

using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Queries.Projects
{
    public class SearchProjectsQuery : IRequest<IEnumerable<Project>>
    {
        public string Keyword { get; set; } = string.Empty;
    }
}