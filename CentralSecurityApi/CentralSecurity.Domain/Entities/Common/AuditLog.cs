
namespace CentralSecurity.Domain.Entities.Common
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string TableName { get; set; }
        public Guid? UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
    }
}
