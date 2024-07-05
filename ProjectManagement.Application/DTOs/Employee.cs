namespace ProjectManagement.Application.DTOs
{
    public class EmployeeDto : BaseEntityDto
    {
        public string Visa { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<ProjectDto> Projects { get; set; }

        public EmployeeDto()
        {
            Visa = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Projects = [];
        }
    }
}