
namespace CentralSecurity.Api.Models.Input
{
    public class RolLoginInput
    {
        public Guid RolId { get; set; }
        public string RoleName { get; set; }
    }
    public class LoginInput
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string? Token { get; set; }
        public IEnumerable<RolLoginInput> Roles { get; set; }
    }
}
