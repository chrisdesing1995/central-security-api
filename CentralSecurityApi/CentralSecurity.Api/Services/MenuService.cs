using AutoMapper;
using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Api.Services.Interfaces;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Entities;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Api.Services
{
    public class MenuService: IMenuService
    {
        private readonly IMapper _mapper;
        private readonly IMenuCommands _menuCommands;
        private readonly IMenuQueries _menuQueries;

        public MenuService(IMapper mapper, IMenuCommands menuCommands, IMenuQueries menuQueries)
        {
            _mapper = mapper;
            _menuCommands = menuCommands;
            _menuQueries = menuQueries;
        }

        public async Task<IEnumerable<MenuOutput>> GetAllMenuAsync()
        {
            try
            {
                var mmenusType = await _menuQueries.GetAllMenuAsync();

                return _mapper.Map<IEnumerable<MenuOutput>>(mmenusType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de menu " + ex.Message);
            }
        }

        public async Task<MenuOutput> GetMenuByIdAsync(Guid menuId)
        {
            try
            {
                var menuType = await _menuQueries.GetMenuByIdAsync(menuId);

                return _mapper.Map<MenuOutput>(menuType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener menu por Id " + ex.Message);
            }
        }

        public async Task<IEnumerable<MenuOutput>> GetMenuByUserAsync(Guid userId)
        {
            try
            {
                var menuType = await _menuQueries.GetMenuByUserAsync(userId);

                return _mapper.Map<IEnumerable<MenuOutput>>(menuType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener menu por usuario " + ex.Message);
            }
        }

        public async Task<IEnumerable<MenuOutput>> GetMenuByRolAsync(Guid rolId)
        {
            try
            {
                var menuType = await _menuQueries.GetMenuByRolAsync(rolId);

                return _mapper.Map<IEnumerable<MenuOutput>>(menuType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener menu por rol " + ex.Message);
            }
        }

        public async Task<ResultSp> CreateMenuAsync(MenuInput menu)
        {
            try
            {
                var menuType = _mapper.Map<MenuType>(menu);
                var result = await _menuCommands.CreateMenuAsync(menuType);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario " + ex.Message);
            }
        }

        public async Task<ResultSp> UpdateMenuAsync(MenuInput menu)
        {
            try
            {
                var menuType = _mapper.Map<MenuType>(menu);
                var result = await _menuCommands.UpdateMenuAsync(menuType);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario " + ex.Message);
            }
        }

        public Task<ResultSp> DeleteMenuAsync(Guid menuId)
        {
            throw new NotImplementedException();
        }
    }
}
