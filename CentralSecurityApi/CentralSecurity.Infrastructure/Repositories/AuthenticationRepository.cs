using AutoMapper;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Infrastructure.Constant;
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

        public async Task<UserLoginDto> GetUserByUsername(LoginDto input)
        {
            try
            {
                var parameters = Param(input);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_USER_LOGIN;
                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.Login
                  .FromSqlRaw(query, parameters.ToArray())
                  .AsEnumerable()
                  .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario " + ex.Message);
            }
        }

        public List<SqlParameter> Param(LoginDto input)
        {
            var resultParam = new List<SqlParameter>()
            {
                new SqlParameter("@UserName", input.UserName)
            };

            return resultParam;
        }

    }
}
