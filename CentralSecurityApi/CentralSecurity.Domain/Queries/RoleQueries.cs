using AutoMapper;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Queries
{
    public class RoleQueries:IRoleQueries
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public RoleQueries(IMapper mapper, IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleType>> GetAllRoleAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllRoleAsync();

                return _mapper.Map<IEnumerable<RoleType>>(roles);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles " + ex.Message);
            }
        }

        public async Task<RoleType> GetAllRoleByIdAsync(Guid roleId)
        {
            try
            {
                var rol = await _roleRepository.GetAllRoleByIdAsync(roleId);

                return _mapper.Map<RoleType>(rol);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener rol por Id " + ex.Message);
            }
        }
    }
}
