namespace ProjectManagement.Application.DTOs
{
    public class BaseEntityDto
    {
        public Guid Id { get; set; }
        public byte[]? Timestamp { get; set; }
    }
}