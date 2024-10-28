using AutoMapper;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CentralSecurity.Domain.Commands
{
    public class AuthenticationCommands : IAuthenticationCommands
    {
        private IMapper _mapper;
        private IConfiguration _configuration;
        private IAuthenticationRepository _authenticationRepository;

        public AuthenticationCommands(IMapper mapper, IConfiguration configuration, IAuthenticationRepository authenticationRepository)
        {
            _mapper = mapper;
            _configuration = configuration;
            _authenticationRepository = authenticationRepository;
        }

        public async Task<ResponseResult<UserLoginType>> AuthenticateLogin(LoginType commands)
        {
            try
            {
                var loginDto = _mapper.Map<LoginDto>(commands);
                var _ = await _authenticationRepository.GetUserByUsername(loginDto);
                var userDto = _.Result;
                var verfiPassword = CommonService.ConverToDecrypt(userDto.Password) == commands.Password;

                if (!verfiPassword)
                {
                    return new ResponseResult<UserLoginType>("Contraseña incorrecta", false);
                }
                string jwtToken = GenerateToken(userDto);
                userDto.Token = jwtToken;

                var userQuery = _mapper.Map<UserLoginType>(userDto);

                return new ResponseResult<UserLoginType>(userQuery);

            }
            catch (Exception ex) {
                return new ResponseResult<UserLoginType>($"Error al obtener usuario: {ex.Message}", false);
            }
        }

        private string GenerateToken(UserLoginDto data)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, data.Id.ToString()),
                new Claim(ClaimTypes.Name, data.Username),
                new Claim(ClaimTypes.Email, data.Email)
            };

            var valueKey = _configuration.GetSection("Jwt:key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(valueKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

    }
}
