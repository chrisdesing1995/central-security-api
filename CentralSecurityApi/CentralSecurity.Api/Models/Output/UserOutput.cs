﻿
namespace CentralSecurity.Api.Models.Output
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string IsActive { get; set; }
        public string RoleIds { get; set; }
        public string? RoleNames { get; set; }
        public Guid? ObjectFileId { get; set; }
        public string? ObjectFileData { get; set; }
    }
}
