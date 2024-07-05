using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ProjectManagement.Application.Commands.Projects;
using ProjectManagement.Application.DTOs;
using ProjectManagement.Application.Queries.Projects;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IMediator mediator, ILogger<ProjectsController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<Project>> GetAllAsync()
        {
            logger.LogInformation("Get all project async ...");
            var projects = await _mediator.Send(new GetAllProjectQuery { Includes = null });

            logger.LogInformation("Query done!");
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(Guid id)
        {
            static IQueryable<Project> includes(IQueryable<Project> p) =>
            p.Include(project => project.Employees);

            var project = await _mediator.Send(new GetProjectByIdQuery { Id = id, Includes = includes });
            if (project == null)
                return NotFound();

            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Timestamp = project.Timestamp,
                GroupId = project.GroupId,
                ProjectNumber = project.ProjectNumber,
                Name = project.Name,
                Customer = project.Customer,
                Status = project.Status,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                GroupLeaderVisa = project!.Group?.GroupLeader.Visa!,
                GroupLeaderName = $"{project!.Group!.GroupLeader.FirstName!} {project!.Group!.GroupLeader.LastName!}"
            };

            var listEmpDtos = new List<EmployeeDto>();
            foreach (var emp in project!.Employees!)
            {
                listEmpDtos.Add(new EmployeeDto
                {
                    Id = emp.Id,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Visa = emp.Visa
                });
            }

            projectDto.Employees = listEmpDtos;

            return Ok(projectDto);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Project>>> SearchProjects([FromQuery] string keyword)
        {
            var projects = await _mediator.Send(new SearchProjectsQuery { Keyword = keyword });

            if (!projects.Any())
                return NotFound();

            return Ok(projects);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProject([FromBody] CreateProjectCommand command)
        {
            var projectId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProjectById), new { id = projectId }, projectId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            await _mediator.Send(new DeleteProjectCommand { Id = id });
            return NoContent();
        }
    }
}