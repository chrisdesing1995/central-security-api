using AutoMapper;
using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Api.Services.Interfaces;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Api.Services
{
    public class RoleService:IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleCommands _roleCommands;
        private readonly IRoleQueries _roleQueries;

        public RoleService(IMapper mapper, IRoleCommands roleCommands, IRoleQueries roleQueries)
        {
            _mapper = mapper;
            _roleCommands = roleCommands;
            _roleQueries = roleQueries;
        }

        public async Task<IEnumerable<RoleOutput>> GetAllRoleAsync()
        {
            try
            {
                var rolesType = await _roleQueries.GetAllRoleAsync();

                return _mapper.Map<IEnumerable<RoleOutput>>(rolesType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles " + ex.Message);
            }
        }

        public async Task<RoleOutput> GetAllRoleByIdAsync(Guid roleId)
        {
            try
            {
                var rolType = await _roleQueries.GetAllRoleByIdAsync(roleId);

                return _mapper.Map<RoleOutput>(rolType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener rol por Id " + ex.Message);
            }
        }

        public async Task<ResultSp> CreateRoleAsync(RoleInput role)
        {
            try
            {
                var roleType = _mapper.Map<RoleType>(role);
                var result = await _roleCommands.CreateRoleAsync(roleType);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el rol " + ex.Message);
            }
        }

        public async Task<ResultSp> UpdateRoleAsync(RoleInput role)
        {
            try
            {
                var roleType = _mapper.Map<RoleType>(role);
                var result = await _roleCommands.UpdateRoleAsync(roleType);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al Actualizar el rol " + ex.Message);
            }
        }

        public Task<ResultSp> DeleteRoleAsync(Guid roleId)
        {
            throw new NotImplementedException();
        }
    }
}
