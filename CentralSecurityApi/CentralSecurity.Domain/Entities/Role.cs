﻿
namespace CentralSecurity.Domain.Entities
{
    public class Role : AuditableEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }

    }
}
