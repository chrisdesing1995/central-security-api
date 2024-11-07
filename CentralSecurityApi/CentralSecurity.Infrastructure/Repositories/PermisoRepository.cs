using AutoMapper;
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
    public class PermisoRepository: IPermisoRepository
    {
        private readonly CentralSecurityDbContext _dbContext;
        private readonly IMapper _mapper;

        public PermisoRepository(CentralSecurityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResultSp> CreatePermisoAsync(RoleMenuDto roleMenu)
        {
            try
            {
                var parameters = Param(roleMenu);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                var storedProcedureName = Const.StoreProcedure.SP_INSERT_ROLMENU;
                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.ResultSp
                    .FromSqlRaw(query, parameters.ToArray())
                    .AsEnumerable()
                    .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el permiso: " + ex.Message);
            }
        }
        public List<SqlParameter> Param(RoleMenuDto input)
        {
            var resultParam = new List<SqlParameter>()
            {
                new SqlParameter("@RoleId", input.RoleId),
                new SqlParameter("@MenuIds", input.MenusId ?? SqlString.Null),
                new SqlParameter("@UserCreated", input.UserCreated ?? SqlString.Null)
            };

            return resultParam;
        }

    }
}
