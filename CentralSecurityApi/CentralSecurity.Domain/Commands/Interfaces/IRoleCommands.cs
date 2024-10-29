

using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands.Interfaces
{
    public interface IRoleCommands
    {
        Task<ResultSp> CreateRoleAsync(RoleType roleType);
        Task<ResultSp> UpdateRoleAsync(RoleType roleType);
        Task<ResultSp> DeletedRoleAsync(Guid id);
    }

}
