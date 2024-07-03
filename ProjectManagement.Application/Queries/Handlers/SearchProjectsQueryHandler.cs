﻿using MediatR;

using ProjectManagement.Application.Interfaces;
using ProjectManagement.Application.Queries.Projects;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Queries.Handlers
{
    public class SearchProjectsQueryHandler(IProjectRepository projectRepository) : IRequestHandler<SearchProjectsQuery, IEnumerable<Project>>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<IEnumerable<Project>> Handle(SearchProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.SearchAsync(request.Keyword);
        }
    }
}