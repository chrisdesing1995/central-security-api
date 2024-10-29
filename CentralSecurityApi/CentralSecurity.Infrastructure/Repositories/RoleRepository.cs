﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public RoleRepository(CentralSecurityDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoleAsync()
        {
            try
            {
                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_ROLES;

                var dataResult = _dbContext.Roles
                      .FromSqlRaw($"EXEC {storedProcedureName}")
                      .AsEnumerable()
                      .ToList();

                var roles = _mapper.Map<IEnumerable<RoleDto>>(dataResult);
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles " + ex.Message);
            }
        }

        public async Task<RoleDto> GetAllRoleByIdAsync(Guid roleId)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Id", roleId),
                };

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_GET_ALL_ROLES;

                var dataResult = _dbContext.Roles
                      .FromSqlRaw($"EXEC {storedProcedureName}")
                      .AsEnumerable()
                      .FirstOrDefault();

                return _mapper.Map<RoleDto>(dataResult); ;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los roles por Id " + ex.Message);
            }
        }

        public async Task<ResultSp> CreateRoleAsync(RoleDto role)
        {
            try
            {
                var parameters = Param(role, Const.Accions.INSERT);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_INSERT_UPDATE_ROL;

                var dataResult = _dbContext.ResultSp
                .FromSqlRaw($"EXEC {storedProcedureName}"," ", parameters.ToArray())
                .AsEnumerable()
                .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el rol " + ex.Message);
            }
        }

        public async Task<ResultSp> UpdateRoleAsync(RoleDto role)
        {
            try
            {
                var parameters = Param(role, Const.Accions.UPDATE);

                string paramsString = string.Join(",", parameters.Select(x => x.ParameterName));

                string storedProcedureName = Const.StoreProcedure.SP_INSERT_UPDATE_ROL;

                var dataResult = _dbContext.ResultSp
                .FromSqlRaw($"EXEC {storedProcedureName}", " ", parameters.ToArray())
                .AsEnumerable()
                .FirstOrDefault();

                return dataResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el rol " + ex.Message);
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
                new SqlParameter("@UserCreated", input.UserCreated ?? SqlString.Null),
                new SqlParameter("@CreatedAt", input.CreatedAt ?? SqlDateTime.Null),
                new SqlParameter("@UserUpdated", input.UserUpdated ?? SqlString.Null),
                new SqlParameter("@UpdatedAt", input.UpdatedAt ?? SqlDateTime.Null),
                new SqlParameter("@Accion", Accion)
            };

            return resultParam;
        }
    }
}
