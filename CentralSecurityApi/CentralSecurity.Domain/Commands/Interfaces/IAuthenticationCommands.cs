using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands.Interfaces
{
    public interface IAuthenticationCommands
    {
        Task<UserLoginType> AuthenticateLogin(LoginType loginType);
    }
}
