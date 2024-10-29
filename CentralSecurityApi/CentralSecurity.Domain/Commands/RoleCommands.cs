

using AutoMapper;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands
{
    public class RoleCommands: IRoleCommands
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IAuditService _auditService;

        public RoleCommands(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<ResultSp> CreateRoleAsync(RoleType roleType)
        {
            try
            {
                var roleDto = _mapper.Map<RoleDto>(roleType);
                roleDto.CreatedAt = DateTime.Now;
                roleDto.UserCreated = "Admin";//_auditService.GetCurrentUserName();
                var result = await _roleRepository.CreateRoleAsync(roleDto);
            
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el rol " + ex.Message);
            }
        }
        public async Task<ResultSp> UpdateRoleAsync(RoleType roleType)
        {
            try
            {
                var roleDto = _mapper.Map<RoleDto>(roleType);
                roleDto.UpdatedAt = DateTime.Now;
                roleDto.UserUpdated = _auditService.GetCurrentUserName();
                var result = await _roleRepository.UpdateRoleAsync(roleDto);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el rol " + ex.Message);
            }
        }

        public Task<ResultSp> DeletedRoleAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
