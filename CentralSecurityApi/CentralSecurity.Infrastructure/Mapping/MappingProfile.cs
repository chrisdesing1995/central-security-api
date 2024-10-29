﻿
using AutoMapper;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Entities;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Infrastructure.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            #region Mapper Dto and Entity
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
            #endregion

            #region Mapper Dto and type
            CreateMap<LoginDto, LoginType>();
            CreateMap<LoginType, LoginDto>();

            CreateMap<UserLoginDto, UserLoginType>();
            CreateMap<UserLoginType, UserLoginDto>();

            CreateMap<RoleDto, RoleType>();
            CreateMap<RoleType, RoleDto>();
            #endregion

        }
    }
}
