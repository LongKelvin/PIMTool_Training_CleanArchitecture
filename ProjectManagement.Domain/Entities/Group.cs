namespace ProjectManagement.Domain.Entities
{
    public class Group : BaseEntity
    {
        public Guid GroupLeaderId { get; set; }

        public string Name { get; set; }

        public Employee GroupLeader { get; set; }
        public ICollection<Project> Projects { get; set; }

        public Group()
        {
            Projects = [];
            GroupLeader = new Employee();
            Name = string.Empty;
        }

        public Group(Guid groupLeaderId)
        {
            Name = string.Empty;
            GroupLeaderId = groupLeaderId;
            Projects = [];
        }
    }
}