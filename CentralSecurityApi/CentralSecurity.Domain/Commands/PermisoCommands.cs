using AutoMapper;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands
{
    public class PermisoCommands: IPermisoCommands
    {
        private readonly IMapper _mapper;
        private readonly IPermisoRepository _permisoRepository;
        private readonly IAuditService _auditService;

        public PermisoCommands(IMapper mapper, IPermisoRepository permisoRepository, IAuditService auditService)
        {
            _mapper = mapper;
            _permisoRepository = permisoRepository;
            _auditService = auditService;
        }

        public async Task<ResultSp> CreatePermisoAsync(RoleMenuType roleMenuType)
        {
            try
            {
                var roleMenuDto = _mapper.Map<RoleMenuDto>(roleMenuType);
                roleMenuDto.UserCreated = _auditService.GetCurrentUserName();
                var result = await _permisoRepository.CreatePermisoAsync(roleMenuDto);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el permiso " + ex.Message);
            }
        }
    }
}
