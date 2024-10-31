using AutoMapper;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands.Interfaces
{
    public class MenuCommands : IMenuCommands
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;
        private readonly IAuditService _auditService;

        public MenuCommands(IMapper mapper, IMenuRepository menuRepository, IAuditService auditService)
        {
            _mapper = mapper;
            _menuRepository = menuRepository;
            _auditService = auditService;
        }

        public async Task<ResultSp> CreateMenuAsync(MenuType menuType)
        {
            try
            {
                var menuDto = _mapper.Map<MenuDto>(menuType);
                menuDto.CreatedAt = DateTime.Now;
                menuDto.UserCreated = _auditService.GetCurrentUserName();
                var result = await _menuRepository.CreateMenuAsync(menuDto);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el item menu " + ex.Message);
            }
        }

        public async Task<ResultSp> UpdateMenuAsync(MenuType menuType)
        {
            try
            {
                var menuDto = _mapper.Map<MenuDto>(menuType);
                menuDto.CreatedAt = DateTime.Now;
                menuDto.UserCreated = _auditService.GetCurrentUserName();
                var result = await _menuRepository.UpdateMenuAsync(menuDto);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el item menu " + ex.Message);
            }
        }

        public Task<ResultSp> DeletedMenuAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
