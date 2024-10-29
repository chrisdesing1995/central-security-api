
namespace CentralSecurity.Api.Models.Input
{
    public class UserInput
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public string UserCreateAt {  get; set; }
    }
}
