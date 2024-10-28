
namespace CentralSecurity.Domain.Dto
{
    public class RolLoginDto
    {
        public Guid RolId { get; set; }
        public string RoleName { get; set; }
    }

    public class UserLoginDto:AuditableEntityDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string? Token { get; set; }
        public IEnumerable<RolLoginDto> Roles { get; set; }
    }
}
