using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Domain.Common;

namespace CentralSecurity.Api.Services.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuOutput>> GetAllMenuAsync();
        Task<MenuOutput> GetMenuByIdAsync(Guid menuId);
        Task<IEnumerable<MenuOutput>> GetMenuByUserAsync(Guid userId);
        Task<IEnumerable<MenuOutput>> GetMenuByRolAsync(Guid rolId);
        Task<ResultSp> CreateMenuAsync(MenuInput menu);
        Task<ResultSp> UpdateMenuAsync(MenuInput menu);
        Task<ResultSp> DeleteMenuAsync(Guid menuId);
    }
}
