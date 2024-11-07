using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;

namespace CentralSecurity.Domain.Interfaces.Repositories
{
    public interface IPermisoRepository
    {
        Task<ResultSp> CreatePermisoAsync(RoleMenuDto roleMenu);
    }
}
