using AutoMapper;
using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Api.Models.Mapping
{
    public class MappingProfileApi: Profile
    {
        public MappingProfileApi()
        {
            #region Mapper Output and Type
            CreateMap<RoleOutput, RoleType>();
            CreateMap<RoleType, RoleOutput>();
            #endregion

            #region Mapper Input and Type
            CreateMap<RoleInput, RoleType>();
            CreateMap<RoleType, RoleInput>();
            #endregion

        }
    }
}
