using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Infrastructure.Data.PersistenceModels
{
    [Table("Employees")]
    public class EmployeeEntity : BaseEntity
    {
        [Required]
        [StringLength(3)]
        public string Visa { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public virtual ICollection<ProjectEntity> Projects { get; set; }

        public EmployeeEntity()
        {
            Visa = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            BirthDate = DateTime.MinValue;
            Projects = [];
        }

        public override string ToString()
        {
            return $"EmployeeEntity ID: {Id}, Visa: {Visa}, Name: {FirstName} {LastName}, Birth Date: {BirthDate}";
        }
    }
}