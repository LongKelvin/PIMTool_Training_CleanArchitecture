namespace ProjectManagement.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Visa { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public Employee()
        {
            Visa = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Projects = [];
        } // EF Core requires an empty constructor

        public Employee(string visa, string firstName, string lastName, DateTime birthDate)
        {
            Visa = visa;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Projects = [];
        }
    }
}