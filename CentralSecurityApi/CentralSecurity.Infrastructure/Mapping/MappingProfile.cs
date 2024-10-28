
using AutoMapper;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Infrastructure.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            #region Module Security
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
