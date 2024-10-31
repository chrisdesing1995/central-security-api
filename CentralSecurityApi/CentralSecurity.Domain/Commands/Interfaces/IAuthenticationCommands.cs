using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands.Interfaces
{
    public interface IAuthenticationCommands
    {
        Task<UserType> AuthenticateLogin(LoginType loginType);
    }
}
