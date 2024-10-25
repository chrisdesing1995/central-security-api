

namespace CentralSecurity.Domain.Entities
{
    public class Menu : AuditableEntity
    {
        public string MenuName { get; set; }
        public Guid? ParentId { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int SortOrder { get; set; }
        public string IsActive { get; set; }

        // Relaciones
        //public ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
