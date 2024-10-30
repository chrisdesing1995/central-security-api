
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;

namespace CentralSecurity.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserSpDto>> GetAllUserAsync();
        Task<UserSpDto> GetUserByIdAsync(Guid userId);
        Task<ResultSp> CreateUserAsync(UserDto user);
        Task<ResultSp> UpdateUserAsync(UserDto user);
        Task<ResultSp> DeleteUserAsync(Guid userId);
    }
}
