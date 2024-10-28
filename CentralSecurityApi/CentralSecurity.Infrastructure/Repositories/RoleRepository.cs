using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Infrastructure.Constant;
using CentralSecurity.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace CentralSecurity.Infrastructure.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly CentralSecurityDbContext _dbContext;

        public RoleRepository(CentralSecurityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultSp> CreateRoleAsync(RoleDto role)
        {
            try
            {
                var parameters = Param(role, Conts.Accions.INSERT);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Conts.StoreProcedure.SP_INSERT_UPDATE_ROL;

                var dataResult = _dbContext.ResultSp
                .FromSqlRaw($"EXEC {storedProcedureName}"," ", parameters.ToArray())
                .AsEnumerable()
                .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResultSp> UpdateRoleAsync(RoleDto role)
        {
            try
            {
                var parameters = Param(role, Conts.Accions.UPDATE);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Conts.StoreProcedure.SP_INSERT_UPDATE_ROL;

                var dataResult = _dbContext.ResultSp
                .FromSqlRaw($"EXEC {storedProcedureName}", " ", parameters.ToArray())
                .AsEnumerable()
                .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<ResultSp> DeleteRoleAsync(Guid roleId)
        {
            throw new NotImplementedException();
        }

        public List<SqlParameter> Param(RoleDto input, string Accion)
        {
            var resultParam = new List<SqlParameter>()
            {
                new SqlParameter("@Id", input.Id != Guid.Empty ? input.Id : Guid.NewGuid()),
                new SqlParameter("@RolName", input.RoleName),
                new SqlParameter("@Description", input.Description ?? SqlString.Null),
                new SqlParameter("@UserCreated", input.UserCreated),
                new SqlParameter("@CreatedAt", input.CreatedAt),
                new SqlParameter("@UserUpdated", input.UserUpdated ?? SqlString.Null),
                new SqlParameter("@UpdatedAt", input.UpdatedAt ??SqlDateTime.Null),
                new SqlParameter("@Accion", Accion)
            };

            return resultParam;
        }
    }
}
