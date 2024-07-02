using MediatR;

using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Queries.Projects
{
    public class GetAllProjectQuery : IRequest<IEnumerable<Project>>
    {
    }
}