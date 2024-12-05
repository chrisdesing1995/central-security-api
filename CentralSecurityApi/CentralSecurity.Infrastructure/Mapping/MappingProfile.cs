
using AutoMapper;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Entities;
using CentralSecurity.Domain.Types;
using System.Data;

namespace CentralSecurity.Infrastructure.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            #region Mapper Dto and Entity
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDto, Menu>();

            CreateMap<GeneralParameter, GeneralParameterDto>();
            CreateMap<GeneralParameterDto, GeneralParameter>();
            CreateMap<GeneralParameterDetail, GeneralParameterDetailDto>();
            CreateMap<GeneralParameterDetailDto, GeneralParameterDetail>();

            #endregion

            #region Mapper Dto and type
            CreateMap<LoginDto, LoginType>();
            CreateMap<LoginType, LoginDto>();

            CreateMap<UserLoginDto, UserLoginType>();
            CreateMap<UserLoginType, UserLoginDto>();

            CreateMap<UserSpDto, UserType>();
            CreateMap<UserType, UserSpDto>();

            CreateMap<UserDto, UserType>();
            CreateMap<UserType, UserDto>();

            CreateMap<ObjectFileSpDto, ObjectFileType>();
            CreateMap<ObjectFileType, ObjectFileSpDto>();

            CreateMap<ObjectFileDto, ObjectFileType>();
            CreateMap<ObjectFileType, ObjectFileDto>();

            CreateMap<RoleDto, RoleType>();
            CreateMap<RoleType, RoleDto>();

            CreateMap<MenuSpDto, MenuType>();
            CreateMap<MenuType, MenuSpDto>();

            CreateMap<MenuDto, MenuType>();
            CreateMap<MenuType, MenuDto>();

            CreateMap<RoleMenuDto, RoleMenuType>();
            CreateMap<RoleMenuType, RoleMenuDto>();

            CreateMap<GeneralParameterDto, GeneralParameterType>();
            CreateMap<GeneralParameterType, GeneralParameterDto>();
            CreateMap<GeneralParameterDetailDto, GeneralParameterDetailType>();
            CreateMap<GeneralParameterDetailType, GeneralParameterDetailDto>();

            CreateMap<GeneralParameterSpDto, GeneralParameterType>();
            CreateMap<GeneralParameterType, GeneralParameterSpDto>();
            CreateMap<GeneralParameterDetailSpDto, GeneralParameterDetailType>();
            CreateMap<GeneralParameterDetailType, GeneralParameterDetailSpDto>();
            #endregion

        }

    }
}
