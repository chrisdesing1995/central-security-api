
using AutoMapper;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IObjectFileRepository _objectFileRepository;

        public UserQueries(IMapper mapper, IUserRepository userRepository, IObjectFileRepository objectFileRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _objectFileRepository = objectFileRepository;
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
                user.Password = CommonService.ConverToDecrypt(user.Password ?? "");
                var objectFile = await _objectFileRepository.GetObjectFileByEntityAsync(user.Id,"User");

                var userType = _mapper.Map<UserType>(user);
                if (objectFile is not null)
                {
                    userType.ObjectFileId = objectFile.Id;
                    userType.ObjectFileData = objectFile.ObjectData;

                }

                return userType;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por Id " + ex.Message);
            }
        }
    }
}
