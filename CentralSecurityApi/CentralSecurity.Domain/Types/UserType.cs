﻿
namespace CentralSecurity.Domain.Types
{
    public class UserType
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string RoleIds { get; set; }
    }
}