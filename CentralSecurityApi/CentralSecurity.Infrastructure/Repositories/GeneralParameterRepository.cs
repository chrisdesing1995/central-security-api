using AutoMapper;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Infrastructure.Constant;
using CentralSecurity.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlTypes;

namespace CentralSecurity.Infrastructure.Repositories
{
    public class GeneralParameterRepository: IGeneralParameterRepository
    {
        private readonly CentralSecurityDbContext _dbContext;
        private readonly IMapper _mapper;

        public GeneralParameterRepository(CentralSecurityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GeneralParameterSpDto>> GetAllGeneralParameterAsync()
        {
            try
            {
                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_GENERAL_PARAMETERS;

                var dataResult = _dbContext.GeneralParameterSp
                      .FromSqlRaw($"EXEC {storedProcedureName}")
                      .AsEnumerable()
                      .ToList();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de paramétros generales " + ex.Message);
            }
        }

        public async Task<IEnumerable<GeneralParameterDetailSpDto>> GetGeneralParameterByCodeAsync(string code)
        {
            try
            {
                var parameters = ParamCode(code);
                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_GENERAL_PARAMETER_DETAIL_CODE;

                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.GeneralParameterDetailSp
                      .FromSqlRaw(query, parameters.ToArray())
                      .AsEnumerable()
                      .ToList();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener paramétros generales por id " + ex.Message);
            }
        }

        public async Task<GeneralParameterSpDto> GetGeneralParameterByIdAsync(Guid id)
        {
            try
            {
                var parameters = Param(id);
                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_GENERAL_PARAMETER_ID;

                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.GeneralParameterSp
                      .FromSqlRaw(query, parameters.ToArray())
                      .AsEnumerable()
                      .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el lista de los paramétros por codigo " + ex.Message);
            }
        }

        public async Task<ResultSp> CreateGeneralParameterAsync(GeneralParameterDto generalParameter)
        {
            try
            {
                var parameters = Param(generalParameter, Const.Accions.INSERT);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                var storedProcedureName = Const.StoreProcedure.SP_INSERT_UPDATE_GENERAL_PARAMETERS;
                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.ResultSp
                    .FromSqlRaw(query, parameters.ToArray())
                    .AsEnumerable()
                    .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear paramétros generales: " + ex.Message);
            }
        }

        public async Task<ResultSp> UpdateGeneralParameterAsync(GeneralParameterDto generalParameter)
        {
            try
            {
                var parameters = Param(generalParameter, Const.Accions.UPDATE);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                var storedProcedureName = Const.StoreProcedure.SP_INSERT_UPDATE_GENERAL_PARAMETERS;
                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.ResultSp
                    .FromSqlRaw(query, parameters.ToArray())
                    .AsEnumerable()
                    .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario: " + ex.Message);
            }
        }

        public Task<ResultSp> DeleteGeneralParameterAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<SqlParameter> Param(GeneralParameterDto input, string Accion)
        {
            var resultParam = new List<SqlParameter>()
            {
                new SqlParameter("@Id", input.Id != null && input.Id != Guid.Empty ? (object)input.Id : SqlString.Null),
                new SqlParameter("@Code", input.Code),
                new SqlParameter("@Description", input.Description ?? SqlString.Null),
                new SqlParameter("@IsActive", input.IsActive),
                new SqlParameter("@UserCreated", input.UserCreated ?? SqlString.Null),
                new SqlParameter("@UserUpdated", input.UserUpdated ?? SqlString.Null),
                new SqlParameter("@Accion", Accion),
                new SqlParameter("@Detalles", SqlDbType.Structured)
                {
                    TypeName = "dbo.GeneralParameterDetailDto",
                    Value = ConvertToDataTable(input.Details)
                }
            };

            return resultParam;
        }

        public List<SqlParameter> Param(Guid id)
        {
            var resultParam = new List<SqlParameter>()
            {
                new SqlParameter("@Id", id != null && id != Guid.Empty ? (object)id : SqlString.Null)
            };

            return resultParam;
        }

        public List<SqlParameter> ParamCode(string code)
        {
            var resultParam = new List<SqlParameter>()
            {
                new SqlParameter("@Code", code  ?? SqlString.Null)
            };

            return resultParam;
        }

        private DataTable ConvertToDataTable(List<GeneralParameterDetailDto> details)
        {
            var table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Value1", typeof(string));
            table.Columns.Add("Value2", typeof(string));
            table.Columns.Add("Value3", typeof(string));
            table.Columns.Add("Value4", typeof(string));
            table.Columns.Add("Value5", typeof(string));

            if (details != null)
            {
                foreach (var detail in details)
                {
                    table.Rows.Add(
                        detail.Id != Guid.Empty ? (object)detail.Id : DBNull.Value,
                        detail.Code,
                        detail.Value1,
                        detail.Value2,
                        detail.Value3,
                        detail.Value4,
                        detail.Value5
                    );
                }
            }

            return table;
        }


    }
}
