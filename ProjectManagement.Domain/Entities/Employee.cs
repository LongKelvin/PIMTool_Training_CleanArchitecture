namespace ProjectManagement.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Visa { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<ProjectEmployee> ProjectEmployees { get; private set; }

        public Employee()
        {
            Visa = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            ProjectEmployees = [];
        } // EF Core requires an empty constructor

        public Employee(string visa, string firstName, string lastName, DateTime birthDate)
        {
            Visa = visa;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            ProjectEmployees = [];
        }
    }
}