using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands.Interfaces
{
    public interface IAuthenticationCommands
    {
        Task<ResponseResult<UserLoginType>> AuthenticateLogin(LoginType commands);
    }
}
