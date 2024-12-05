using AutoMapper;
using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Api.Models.Mapping
{
    public class MappingProfileApi: Profile
    {
        public MappingProfileApi()
        {
            #region Mapper Output and Type
            CreateMap<LoginOutput, UserLoginType>();
            CreateMap<UserLoginType, LoginOutput>();

            CreateMap<UserOutput, UserType>();
            CreateMap<UserType, UserOutput>();

            CreateMap<ObjectFileOutput, ObjectFileType>();
            CreateMap<ObjectFileType, ObjectFileOutput>();

            CreateMap<RoleOutput, RoleType>();
            CreateMap<RoleType, RoleOutput>();

            CreateMap<MenuOutput, MenuType>();
            CreateMap<MenuType, MenuOutput>();

            CreateMap<GeneralParameterOutput, GeneralParameterType>();
            CreateMap<GeneralParameterType, GeneralParameterOutput>();

            CreateMap<GeneralParameterDetailOutput, GeneralParameterDetailType>();
            CreateMap<GeneralParameterDetailType, GeneralParameterDetailOutput>();

            #endregion

            #region Mapper Input and Type
            CreateMap<LoginInput, LoginType>();
            CreateMap<LoginType, LoginInput>();

            CreateMap<UserInput, UserType>();
            CreateMap<UserType, UserInput>();

            CreateMap<ObjectFileInput, ObjectFileType>();
            CreateMap<ObjectFileType, ObjectFileInput>();

            CreateMap<RoleInput, RoleType>();
            CreateMap<RoleType, RoleInput>();

            CreateMap<MenuInput, MenuType>();
            CreateMap<MenuType, MenuInput>();

            CreateMap<RoleMenuInput, RoleMenuType>();
            CreateMap<RoleMenuType, RoleMenuInput>();

            CreateMap<GeneralParameterInput, GeneralParameterType>();
            CreateMap<GeneralParameterType, GeneralParameterInput>();

            CreateMap<GeneralParameterDetailInput, GeneralParameterDetailType>();
            CreateMap<GeneralParameterDetailType, GeneralParameterDetailInput>();
            #endregion

        }
    }
}
