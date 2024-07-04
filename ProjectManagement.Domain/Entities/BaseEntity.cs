using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Timestamp]
        [Column(TypeName = "rowversion")]
        public byte[]? Timestamp { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}