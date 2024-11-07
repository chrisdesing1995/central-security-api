using CentralSecurity.Api.Models.Input;
using CentralSecurity.Domain.Common;

namespace CentralSecurity.Api.Services.Interfaces
{
    public interface IPermisoService
    {
        Task<ResultSp> CreatePermisoAsync(RoleMenuInput roleMenu);
    }
}
