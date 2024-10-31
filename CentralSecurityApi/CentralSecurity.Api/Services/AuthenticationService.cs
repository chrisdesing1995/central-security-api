using AutoMapper;
using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Api.Services.Interfaces;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Entities;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Api.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationCommands _authenticationCommands;

        public AuthenticationService(IMapper mapper, IAuthenticationCommands authenticationCommands)
        {
            _mapper = mapper;
            _authenticationCommands = authenticationCommands;
        }

        public async Task<LoginOutput> AuthenticateLogin(LoginInput input)
        {
            try
            {
                var loginType = _mapper.Map<LoginType>(input);
                var result = await _authenticationCommands.AuthenticateLogin(loginType);

                return _mapper.Map<LoginOutput>(result);
            }
            catch (Exception ex) {
                throw new Exception("Error al autenticarse " + ex.Message);
            }
        }
    }
}
