
namespace CentralSecurity.Api.Models.Output
{
    public class RolUserOutput
    {
        public Guid RolId { get; set; }
        public string RoleName { get; set; }
    }
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string? Token { get; set; }
        public IEnumerable<RolUserOutput> Roles { get; set; }
    }
}
