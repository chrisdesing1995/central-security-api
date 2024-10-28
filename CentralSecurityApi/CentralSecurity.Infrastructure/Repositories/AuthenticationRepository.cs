using AutoMapper;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace CentralSecurity.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly CentralSecurityDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public AuthenticationRepository(CentralSecurityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseResult<UserLoginDto>> GetUserByUsername(LoginDto input)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@UserName", input.UserName),
                };

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Conts.Conts.StoreProcedure.SP_GET_ALL_USER_LOGIN;

                var dataResult = await _dbContext.Login
                  .FromSqlRaw($"EXEC {storedProcedureName} @UserName", parameters.ToArray())
                  .AsNoTracking()
                  .ToListAsync();

                var user = dataResult.FirstOrDefault();
                if (user == null)
                {
                    return new ResponseResult<UserLoginDto>("No existe usuario", false);
                }

                return new ResponseResult<UserLoginDto>(user);
            }
            catch (Exception ex)
            {
                return new ResponseResult<UserLoginDto>($"Error al obtener usuario: {ex.Message}", false);
            }
        }
    }
}
