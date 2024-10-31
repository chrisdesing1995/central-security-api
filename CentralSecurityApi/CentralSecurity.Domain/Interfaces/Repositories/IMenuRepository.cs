
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;

namespace CentralSecurity.Domain.Interfaces.Repositories
{
    public interface IMenuRepository
    {
        Task<IEnumerable<MenuSpDto>> GetAllMenuAsync();
        Task<MenuSpDto> GetMenuByIdAsync(Guid menuId);
        Task<IEnumerable<MenuSpDto>> GetMenuByUserAsync(Guid userId);
        Task<ResultSp> CreateMenuAsync(MenuDto menu);
        Task<ResultSp> UpdateMenuAsync(MenuDto menu);
        Task<ResultSp> DeleteMenuAsync(Guid menuId);
    }
}
