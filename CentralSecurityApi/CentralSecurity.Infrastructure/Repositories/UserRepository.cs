
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
    public class UserRepository: IUserRepository
    {
        private readonly CentralSecurityDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(CentralSecurityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

                var users = _mapper.Map<IEnumerable<UserSpDto>>(dataResult);
                return users;
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
                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_USER_ID;

                var dataResult = _dbContext.UserSp
                      .FromSqlRaw($"EXEC {storedProcedureName}")
                      .AsEnumerable()
                      .ToList();

                var user = _mapper.Map<UserSpDto>(dataResult);
                return user;
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
                new SqlParameter("@Username", input.Username),
                new SqlParameter("@Pasword", input.Password),
                new SqlParameter("@Email", input.Email),
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

    }
}
