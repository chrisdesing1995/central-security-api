
using AutoMapper;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Entities;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Infrastructure.Constant;
using CentralSecurity.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace CentralSecurity.Infrastructure.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly CentralSecurityDbContext _dbContext;
        private readonly IMapper _mapper;

        public MenuRepository(CentralSecurityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuSpDto>> GetAllMenuAsync()
        {
            try
            {
                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_MENU;

                var dataResult = _dbContext.MenuSp
                      .FromSqlRaw($"EXEC {storedProcedureName}")
                      .AsEnumerable()
                      .ToList();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de menu " + ex.Message);
            }
        }

        public async Task<MenuSpDto> GetMenuByIdAsync(Guid menuId)
        {
            try
            {
                var parameters = Param(menuId);
                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_MENU_ID;

                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.MenuSp
                      .FromSqlRaw(query, parameters.ToArray())
                      .AsEnumerable()
                      .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el menu por id " + ex.Message);
            }
        }

        public async Task<IEnumerable<MenuSpDto>> GetMenuByUserAsync(Guid userId)
        {
            try
            {
                var parameters = ParamMenuUser(userId);
                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_MENU_USER;

                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.MenuSp
                      .FromSqlRaw(query, parameters.ToArray())
                      .AsEnumerable()
                      .ToList();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el menu por id " + ex.Message);
            }
        }

        public async Task<ResultSp> CreateMenuAsync(MenuDto menu)
        {
            try
            {
                var parameters = Param(menu, Const.Accions.INSERT);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                var storedProcedureName = Const.StoreProcedure.SP_INSERT_UPDATE_MENU;
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

        public async Task<ResultSp> UpdateMenuAsync(MenuDto menu)
        {
            try
            {
                var parameters = Param(menu, Const.Accions.INSERT);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                var storedProcedureName = Const.StoreProcedure.SP_INSERT_UPDATE_MENU;
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

        public Task<ResultSp> DeleteMenuAsync(Guid menuId)
        {
            throw new NotImplementedException();
        }

        public List<SqlParameter> Param(MenuDto input, string Accion)
        {
            var resultParam = new List<SqlParameter>()
            {
                new SqlParameter("@Id", input.Id != null && input.Id != Guid.Empty ? (object)input.Id : SqlString.Null),
                new SqlParameter("@MenuName", input.MenuName),
                new SqlParameter("@ParentId", input.ParentId != null && input.ParentId != Guid.Empty ? (object)input.ParentId : SqlString.Null),
                new SqlParameter("@Url", input.Url),
                new SqlParameter("@Icon", input.Icon),
                new SqlParameter("@SortOrder", input.SortOrder),
                new SqlParameter("@IsActive", input.IsActive),
                new SqlParameter("@CreatedAt", input.CreatedAt ?? SqlDateTime.Null),
                new SqlParameter("@UserCreated", input.UserCreated ?? SqlString.Null),
                new SqlParameter("@UpdatedAt", input.UpdatedAt ?? SqlDateTime.Null),
                new SqlParameter("@UserUpdated", input.UserUpdated ?? SqlString.Null),
                new SqlParameter("@Accion", Accion)
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

        public List<SqlParameter> ParamMenuUser(Guid userId)
        {
            var resultParam = new List<SqlParameter>()
            {
                new SqlParameter("@UserId", userId != null && userId != Guid.Empty ? (object)userId : SqlString.Null)
            };

            return resultParam;
        }

    }
}
