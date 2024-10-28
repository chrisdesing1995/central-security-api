
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;

namespace CentralSecurity.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<ResultSp> CreateRoleAsync(RoleDto role);
        Task<ResultSp> UpdateRoleAsync(RoleDto role);
        Task<ResultSp> DeleteRoleAsync(Guid roleId);
    }
}
