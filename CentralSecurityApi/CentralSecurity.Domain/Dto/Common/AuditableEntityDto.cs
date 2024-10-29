
namespace CentralSecurity.Domain.Dto
{
    public class AuditableEntityDto
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UserCreated { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UserUpdated { get; set; }
    }
}
