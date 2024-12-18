﻿
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
    public class UserRepository : IUserRepository
    {
        private readonly CentralSecurityDbContext _dbContext;

        public UserRepository(CentralSecurityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<UserSpDto>> GetAllUserAsync()
        {
            try
            {
                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_USERS;

                var dataResult = _dbContext.UserSp
                      .FromSqlRaw($"EXEC {storedProcedureName}")
                      .AsEnumerable()
                      .ToList();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles " + ex.Message);
            }
        }

        public async Task<UserSpDto> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var parameters = Param(userId);
                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_USER_ID;

                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.UserSp
                      .FromSqlRaw(query, parameters.ToArray())
                      .AsEnumerable()
                      .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles " + ex.Message);
            }
        }

        public async Task<ResultSp> CreateUserAsync(UserDto user)
        {
            try
            {
                var parameters = Param(user, Const.Accions.INSERT);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                var storedProcedureName = Const.StoreProcedure.SP_INSERT_UPDATE_USER;
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

        public async Task<ResultSp> UpdateUserAsync(UserDto user)
        {
            try
            {
                var parameters = Param(user, Const.Accions.UPDATE);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                var storedProcedureName = Const.StoreProcedure.SP_INSERT_UPDATE_USER;
                var query = $"EXEC {storedProcedureName} {paramsString}";

                var dataResult = _dbContext.ResultSp
                    .FromSqlRaw(query, parameters.ToArray())
                    .AsEnumerable()
                    .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario: " + ex.Message);
            }
        }

        public Task<ResultSp> DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<SqlParameter> Param(UserDto input, string Accion)
        {
            var resultParam = new List<SqlParameter>()
            {
                new SqlParameter("@Id", input.Id != null && input.Id != Guid.Empty ? (object)input.Id : SqlString.Null),
                new SqlParameter("@RoleIds", input.RoleIds),
                new SqlParameter("@Name", input.Name),
                new SqlParameter("@SurName", input.SurName),
                new SqlParameter("@Username", input.Username),
                new SqlParameter("@Password", input.Password),
                new SqlParameter("@Email", input.Email),
                new SqlParameter("@Phone", input.Phone ?? SqlString.Null),
                new SqlParameter("@IsActive", input.IsActive),
                new SqlParameter("@ObjectId", input.ObjectFileId != null && input.ObjectFileId != Guid.Empty ? (object)input.ObjectFileId : SqlString.Null),
                new SqlParameter("@ObjectFile", input.ObjectFileData ?? SqlString.Null),
                new SqlParameter("@UserCreated", input.UserCreated ?? SqlString.Null),
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

    }
}
