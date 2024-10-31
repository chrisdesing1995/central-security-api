
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands.Interfaces
{
    public interface IMenuCommands
    {
        Task<ResultSp> CreateMenuAsync(MenuType menuType);
        Task<ResultSp> UpdateMenuAsync(MenuType menuType);
        Task<ResultSp> DeletedMenuAsync(Guid id);
    }
}
