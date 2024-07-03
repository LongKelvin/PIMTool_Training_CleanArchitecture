using MediatR;

using Microsoft.AspNetCore.Mvc;

using ProjectManagement.Application.Commands.Projects;
using ProjectManagement.Application.Queries.Projects;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<Project>> GetAllAsync([FromQuery] bool? enableEagerLoading)
        {
            var projects = await _mediator.Send(
                new GetAllProjectQuery { EnableEagerLoading = enableEagerLoading ?? false });
            if (projects == null)
                return NotFound();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(Guid id)
        {
            var project = await _mediator.Send(new GetProjectByIdQuery { Id = id });
            if (project == null)
                return NotFound();

            return Ok(project);
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