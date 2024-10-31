using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;

namespace CentralSecurity.Domain.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<UserSpDto> GetUserByUsername(LoginDto input);
    }
}
