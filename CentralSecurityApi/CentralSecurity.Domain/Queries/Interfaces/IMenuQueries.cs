
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Queries.Interfaces
{
    public interface IMenuQueries
    {
        Task<IEnumerable<MenuType>> GetAllMenuAsync();
        Task<MenuType> GetMenuByIdAsync(Guid menuId);
        Task<IEnumerable<MenuType>> GetMenuByUserAsync(Guid userId);
        Task<IEnumerable<MenuType>> GetMenuByRolAsync(Guid rolId);
    }
}
