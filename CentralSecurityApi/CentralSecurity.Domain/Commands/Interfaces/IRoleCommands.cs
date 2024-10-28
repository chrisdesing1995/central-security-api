

using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands.Interfaces
{
    public interface IRoleCommands
    {
        Task<ResponseResult<RoleType>> CreateRoleAsync(RoleType roleType);
        Task<ResponseResult<RoleType>> UpdateRoleAsync(RoleType roleType);
        Task<ResponseResult<RoleType>> DeletedRoleAsync(Guid id);
    }

}
