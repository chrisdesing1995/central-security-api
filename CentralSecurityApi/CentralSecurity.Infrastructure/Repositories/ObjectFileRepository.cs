using AutoMapper;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Infrastructure.Constant;
using CentralSecurity.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace CentralSecurity.Infrastructure.Repositories
{
    public class ObjectFileRepository : IObjectFileRepository
    {
        private readonly CentralSecurityDbContext _dbContext;
        private readonly IMapper _mapper;

        public ObjectFileRepository(CentralSecurityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ObjectFileSpDto>> GetAllObjectFileByEntityAsync(Guid? entityId, string? entityName)
        {
            try
            {
                var parameters = Param(entityId, entityName);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_OBJECTFILE_ENTITY;
                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.ObjectFileSp
                      .FromSqlRaw(query, parameters.ToArray())
                      .AsEnumerable()
                      .ToList();

                return _mapper.Map<IEnumerable<ObjectFileSpDto>>(dataResult); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles por Id " + ex.Message);
            }
        }

        public async Task<ObjectFileSpDto> GetObjectFileByEntityAsync(Guid? entityId, string? entityName)
        {
            try
            {
                var parameters = Param(entityId, entityName);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_OBJECTFILE_ENTITY;
                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.ObjectFileSp
                      .FromSqlRaw(query, parameters.ToArray())
                      .AsEnumerable()
                      .FirstOrDefault();

                return _mapper.Map<ObjectFileSpDto>(dataResult); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles por Id " + ex.Message);
            }
        }

        public List<SqlParameter> Param(Guid? entityId, string? entityName)
        {
            var resultParam = new List<SqlParameter>()
            {
                new SqlParameter("@EnityId", entityId != null && entityId != Guid.Empty ? (object)entityId : SqlString.Null),
                new SqlParameter("@EntityName", entityName ?? SqlString.Null)
            };

            return resultParam;
        }
    }
}
