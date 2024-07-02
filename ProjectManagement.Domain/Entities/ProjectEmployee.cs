namespace ProjectManagement.Domain.Entities
{
    public class ProjectEmployee
    {
        public Guid ProjectId { get; private set; }
        public Guid EmployeeId { get; private set; }

        public Project Project { get; private set; }
        public Employee Employee { get; private set; }

        public ProjectEmployee()
        {
        } // EF Core requires an empty constructor

        public ProjectEmployee(Guid projectId, Guid employeeId)
        {
            ProjectId = projectId;
            EmployeeId = employeeId;
        }
    }
}