using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Infrastructure.Data.PersistenceModels
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Timestamp]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte[]? Timestamp { get; set; }
    }
}
