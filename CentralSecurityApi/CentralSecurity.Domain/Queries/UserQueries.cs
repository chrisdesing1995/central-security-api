
using AutoMapper;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserQueries(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserType>> GetAllUserAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUserAsync();

                return _mapper.Map<IEnumerable<UserType>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios " + ex.Message);
            }
        }

        public async Task<UserType> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                return _mapper.Map<UserType>(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por Id " + ex.Message);
            }
        }
    }
}
