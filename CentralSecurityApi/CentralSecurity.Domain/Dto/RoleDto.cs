
namespace CentralSecurity.Domain.Dto
{
    public class RoleDto : AuditableEntityDto
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
