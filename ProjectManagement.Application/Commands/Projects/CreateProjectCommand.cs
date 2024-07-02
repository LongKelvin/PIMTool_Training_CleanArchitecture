using MediatR;

namespace ProjectManagement.Application.Commands.Projects
{
    public class CreateProjectCommand : IRequest<Guid>
    {
        public Guid GroupId { get; set; }
        public int ProjectNumber { get; set; }
        public string? Name { get; set; }
        public string? Customer { get; set; }
        public string? Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}