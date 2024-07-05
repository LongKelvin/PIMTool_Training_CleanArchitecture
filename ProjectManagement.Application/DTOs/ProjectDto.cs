namespace ProjectManagement.Application.DTOs
{
    public class ProjectDto : BaseEntityDto
    {
        public Guid GroupId { get; set; }
        public string GroupLeaderVisa { get; set; }
        public string GroupLeaderName { get; set; }
        public int ProjectNumber { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<EmployeeDto>? Employees { get; set; }

        public ProjectDto()
        {
            Name = string.Empty;
            Customer = string.Empty;
            Status = string.Empty;
            Employees = [];
            GroupLeaderName = string.Empty;
            GroupLeaderVisa = string.Empty;
        }
    }
}