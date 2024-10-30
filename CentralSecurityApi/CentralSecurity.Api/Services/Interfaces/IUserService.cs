using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Domain.Common;

namespace CentralSecurity.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserOutput>> GetAllUserAsync();
        Task<UserOutput> GetUserByIdAsync(Guid userId);
        Task<ResultSp> CreateUserAsync(UserInput user);
        Task<ResultSp> UpdateUserAsync(UserInput user);
        Task<ResultSp> DeleteUserAsync(Guid userId);
    }
}
