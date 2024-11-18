using AutoMapper;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands
{
    public class GeneralParameterCommands: IGeneralParameterCommands
    { 
        private readonly IMapper _mapper;
        private readonly IGeneralParameterRepository _generalParameterRepository;
        private readonly IAuditService _auditService;

        public GeneralParameterCommands(IMapper mapper, IGeneralParameterRepository generalParameterRepository, IAuditService auditService)
        {
            _mapper = mapper;
            _generalParameterRepository = generalParameterRepository;
            _auditService = auditService;
        }

        public async Task<ResultSp> CreateGeneralParamAsync(GeneralParameterType generalParameterTypeType)
        {
            try
            {
                var generelParamDto = _mapper.Map<GeneralParameterDto>(generalParameterTypeType);
                generelParamDto.CreatedAt = DateTime.Now;
                generelParamDto.UserCreated = _auditService.GetCurrentUserName();
                var result = await _generalParameterRepository.CreateGeneralParameterAsync(generelParamDto);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear parametro de sistema " + ex.Message);
            }
        }

        public async Task<ResultSp> UpdateGeneralParamAsync(GeneralParameterType generalParameterTypeType)
        {
            try
            {
                var generelParamDto = _mapper.Map<GeneralParameterDto>(generalParameterTypeType);
                generelParamDto.UpdatedAt = DateTime.Now;
                generelParamDto.UserUpdated = _auditService.GetCurrentUserName();
                var result = await _generalParameterRepository.UpdateGeneralParameterAsync(generelParamDto);

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar parametro de sistema " + ex.Message);
            }
        }
    }
}
