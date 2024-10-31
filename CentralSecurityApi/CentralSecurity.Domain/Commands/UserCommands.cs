
using AutoMapper;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands
{
    public class UserCommands: IUserCommands
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IAuditService _auditService;

        public UserCommands(IMapper mapper, IUserRepository userRepository, IAuditService auditService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _auditService = auditService;
        }

        public async Task<ResultSp> CreateUserAsync(UserType userType)
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(userType);
                userDto.CreatedAt = DateTime.Now;
                userDto.UserCreated = _auditService.GetCurrentUserName();
                userDto.Password = CommonService.ConverToEncrypt(userType.Password ?? "");
                var result = await _userRepository.CreateUserAsync(userDto);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario " + ex.Message);
            }
        }

        public async Task<ResultSp> UpdateUserAsync(UserType userType)
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(userType);
                userDto.UpdatedAt = DateTime.Now;
                userDto.UserUpdated = _auditService.GetCurrentUserName();
                var result = await _userRepository.UpdateUserAsync(userDto);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario " + ex.Message);
            }
        }

        public Task<ResultSp> DeletedUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
