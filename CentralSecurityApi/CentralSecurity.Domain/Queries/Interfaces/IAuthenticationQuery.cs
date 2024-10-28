using CentralSecurity.Domain.Commands.Models;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Queries.Models;

namespace CentralSecurity.Domain.Queries.Interfaces
{
    public interface IAuthenticationQuery
    {
        Task<ResponseResult<LoginQuery>> GetUserByUsername(LoginCommands commands);
    }
}
