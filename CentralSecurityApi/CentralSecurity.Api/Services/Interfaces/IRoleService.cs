using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Domain.Common;

namespace CentralSecurity.Api.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleOutput>> GetAllRoleAsync();
        Task<RoleOutput> GetAllRoleByIdAsync(Guid roleId);
        Task<ResultSp> CreateRoleAsync(RoleInput role);
        Task<ResultSp> UpdateRoleAsync(RoleInput role);
        Task<ResultSp> DeleteRoleAsync(Guid roleId);
    }
}
