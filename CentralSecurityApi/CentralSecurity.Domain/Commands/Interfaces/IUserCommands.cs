
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands.Interfaces
{
    public interface IUserCommands
    {
        Task<ResultSp> CreateUserAsync(UserType userType);
        Task<ResultSp> UpdateUserAsync(UserType userType);
        Task<ResultSp> DeletedUserAsync(Guid id);
    }
}
