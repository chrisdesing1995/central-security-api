
namespace CentralSecurity.Domain.Entities
{
    public class Role : AuditableEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }

        // Relaciones
        //public ICollection<UserRole> UserRoles { get; set; }
        //public ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
