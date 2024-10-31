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

        public async Task<UserType> AuthenticateLogin(LoginType loginType)
        {
            try
            {
                var loginDto = _mapper.Map<LoginDto>(loginType);
                var userDto = await _authenticationRepository.GetUserByUsername(loginDto);
             
                var verfiPassword = CommonService.ConverToDecrypt(userDto.Password) == loginType.Password;

                if (!verfiPassword)
                {
                    throw new Exception("Contraseña incorrecta");
                }
                string jwtToken = GenerateToken(userDto);
                
                var usertype = _mapper.Map<UserType>(userDto);
                usertype.Token = jwtToken;
                return usertype;

            }
            catch (Exception ex) {
                throw new Exception("Error al autenticarse " + ex.Message);
            }
        }

        private string GenerateToken(UserSpDto data)
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
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

    }
}
