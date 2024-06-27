using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Infrastructure.Data.PersistenceModels
{
    [Table("ProjectGroups")]
    public class GroupEntity : BaseEntity
    {
        public int GroupLeaderId { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupName { get; set; }

        [ForeignKey("GroupLeaderId")]
        public virtual EmployeeEntity GroupLeader { get; set; }

        public virtual ICollection<ProjectEntity> Projects { get; set; }

        public GroupEntity()
        {
            Projects = [];
            GroupLeader = new EmployeeEntity();
            GroupName = string.Empty;
        }
    }
}