
using AutoMapper;
using CentralSecurity.Domain.Entities;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Queries
{
    public class MenuQueries: IMenuQueries
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;

        public MenuQueries(IMapper mapper, IMenuRepository menuRepository)
        {
            _mapper = mapper;
            _menuRepository = menuRepository;
        }

        public async Task<IEnumerable<MenuType>> GetAllMenuAsync()
        {
            try
            {
                var menus = await _menuRepository.GetAllMenuAsync();

                return _mapper.Map<IEnumerable<MenuType>>(menus);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de menu " + ex.Message);
            }
        }

        public async Task<MenuType> GetMenuByIdAsync(Guid menuId)
        {
            try
            {
                var user = await _menuRepository.GetMenuByIdAsync(menuId);

                return _mapper.Map<MenuType>(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener menu por Id " + ex.Message);
            }
        }

        public async Task<IEnumerable<MenuType>> GetMenuByUserAsync(Guid userId)
        {
            try
            {
                var menu = await _menuRepository.GetMenuByUserAsync(userId);

                return _mapper.Map<IEnumerable<MenuType>>(menu);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener menu por usuario " + ex.Message);
            }
        }
    }
}
