using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using ProjectManagement.Infrastructure.Data.PersistenceEnums;

namespace ProjectManagement.Infrastructure.Data.PersistenceModels
{
    [Table("Projects")]
    public class ProjectEntity : BaseEntity
    {
        public int GroupId { get; set; }

        [Required]
        public int ProjectNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Customer { get; set; }

        [Required]
        [StringLength(3)]
        public string Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [ForeignKey("GroupId")]
        public virtual GroupEntity Group { get; set; }

        public virtual ICollection<EmployeeEntity> Employees { get; set; }

        public ProjectEntity()
        {
            Name = string.Empty;
            Customer = string.Empty;
            Status = ProjectEntityStatus.NEW.ToString();
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
            Group = new GroupEntity();
            Employees = [];
        }

        public override string ToString()
        {
            return $"ProjectEntity ID: {Id}, Name: {Name}, Customer: {Customer}, Status: {Status}, Start Date: {StartDate}, End Date: {EndDate}";
        }
    }
}
