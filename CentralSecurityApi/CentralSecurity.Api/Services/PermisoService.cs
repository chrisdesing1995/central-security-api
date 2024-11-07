using AutoMapper;
using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Services.Interfaces;
using CentralSecurity.Domain.Commands;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Entities;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Api.Services
{
    public class PermisoService : IPermisoService
    {
        private readonly IMapper _mapper;
        private readonly IPermisoCommands _permisoCommands;

        public PermisoService(IMapper mapper, IPermisoCommands permisoCommands)
        {
            _mapper = mapper;
            _permisoCommands = permisoCommands;
        }

        public async Task<ResultSp> CreatePermisoAsync(RoleMenuInput roleMenu)
        {
            try
            {
                var roleMenuType = _mapper.Map<RoleMenuType>(roleMenu);
                var result = await _permisoCommands.CreatePermisoAsync(roleMenuType);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el permiso " + ex.Message);
            }
        }
    }
}
