using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands.Interfaces
{
    public interface IPermisoCommands
    {
        Task<ResultSp> CreatePermisoAsync(RoleMenuType roleMenuType);
    }
}
