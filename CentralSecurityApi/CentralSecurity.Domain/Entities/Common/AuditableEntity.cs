
namespace CentralSecurity.Domain.Entities
{
    public class AuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UserCreated { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UserUpdated { get; set; }
    }
}
