using ProjectManagement.Domain.Interfaces;

namespace ProjectManagement.Domain.Entities
{
    public class Project : BaseEntity, IAggregateRoot
    {
        public Guid GroupId { get; set; }
        public int ProjectNumber { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Group Group { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }

        public Project()
        {
            Name = string.Empty;
            Customer = string.Empty;
            Status = string.Empty;
            Group = new Group();
        } // EF Core requires an empty constructor

        public Project(Guid groupId, int projectNumber, string name, string customer, string status, DateTime startDate)
        {
            GroupId = groupId;
            ProjectNumber = projectNumber;
            Name = name;
            Customer = customer;
            Status = status;
            StartDate = startDate;
            ProjectEmployees = [];
        }

        public void UpdateDetails(string name, string customer, string status, DateTime startDate, DateTime? endDate)
        {
            Name = name;
            Customer = customer;
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}