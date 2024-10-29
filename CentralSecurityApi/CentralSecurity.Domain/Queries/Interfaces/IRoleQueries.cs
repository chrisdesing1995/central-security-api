using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Queries.Interfaces
{
    public interface IRoleQueries
    {
        Task<IEnumerable<RoleType>> GetAllRoleAsync();
        Task<RoleType> GetAllRoleByIdAsync(Guid roleId);
    }
}
