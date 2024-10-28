
namespace CentralSecurity.Domain.Types
{
    public class UserRolType
    {
        public Guid RolId { get; set; }
        public string RoleName { get; set; }
    }
    public class UserLoginType
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string? Token { get; set; }
        public IEnumerable<UserRolType> Roles { get; set; }
    }
}
