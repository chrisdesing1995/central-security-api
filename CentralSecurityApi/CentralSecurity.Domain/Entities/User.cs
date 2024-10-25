﻿
namespace CentralSecurity.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }

        // Relaciones
        //public ICollection<UserRole> UserRoles { get; set; }
    }
}
