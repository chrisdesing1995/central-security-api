using AutoMapper;
using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Api.Services.Interfaces;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Types;
using System.Data;

namespace CentralSecurity.Api.Services
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserCommands _userCommands;
        private readonly IUserQueries _userQueries;

        public UserService(IMapper mapper, IUserCommands userCommands, IUserQueries userQueries)
        {
            _mapper = mapper;
            _userCommands = userCommands;
            _userQueries = userQueries;
        }
        public async Task<IEnumerable<UserOutput>> GetAllUserAsync()
        {
            try
            {
                var usersType = await _userQueries.GetAllUserAsync();

                return _mapper.Map<IEnumerable<UserOutput>>(usersType);
            }
            catch (Exception ex) {
                throw new Exception("Error al obtener los usuarios " + ex.Message);
            }
        }

        public async Task<UserOutput> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var userType = await _userQueries.GetUserByIdAsync(userId);

                return _mapper.Map<UserOutput>(userType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por Id " + ex.Message);
            }
        }

        public async Task<ResultSp> CreateUserAsync(UserInput user)
        {
            try
            {
                var userType = _mapper.Map<UserType>(user);
                var result = await _userCommands.CreateUserAsync(userType);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario " + ex.Message);
            }
        }

        public async Task<ResultSp> UpdateUserAsync(UserInput user)
        {
            try
            {
                var userType = _mapper.Map<UserType>(user);
                var result = await _userCommands.UpdateUserAsync(userType);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario " + ex.Message);
            }
        }

        public Task<ResultSp> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
