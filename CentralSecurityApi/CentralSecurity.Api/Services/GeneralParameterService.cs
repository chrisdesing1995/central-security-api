using AutoMapper;
using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Api.Services.Interfaces;
using CentralSecurity.Domain.Commands.Interfaces;
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Entities;
using CentralSecurity.Domain.Queries;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Api.Services
{
    public class GeneralParameterService: IGeneralParameterService
    {
        private readonly IMapper _mapper;
        private readonly IGeneralParameterQueries _generalParameterQueries;
        private readonly IGeneralParameterCommands _generalParameterCommands;

        public GeneralParameterService(IMapper mapper,IGeneralParameterQueries generalParameterQueries,IGeneralParameterCommands GeneralParameterCommands)
        {
            _mapper = mapper;
            _generalParameterQueries = generalParameterQueries;
            _generalParameterCommands = GeneralParameterCommands;
        }

        public async Task<IEnumerable<GeneralParameterOutput>> GetAllGeneralParameterAsync()
        {
            try
            {
                var generalParamType = await _generalParameterQueries.GetAllGeneralParameterAsync();

                return _mapper.Map<IEnumerable<GeneralParameterOutput>>(generalParamType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de parametros generales " + ex.Message);
            }
        }

        public async Task<IEnumerable<GeneralParameterOutput>> GetGeneralParameterByCodeAsync(string code)
        {
            try
            {
                var generalParamType = await _generalParameterQueries.GetGeneralParameterByCodeAsync(code);

                return _mapper.Map<IEnumerable<GeneralParameterOutput>>(generalParamType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener parammetros generales por codigo cabecera " + ex.Message);
            }
        }

        public async Task<GeneralParameterOutput> GetGeneralParameterByIdAsync(Guid generalParamId)
        {
            try
            {
                var generalParamType = await _generalParameterQueries.GetGeneralParameterByIdAsync(generalParamId);

                return _mapper.Map<GeneralParameterOutput>(generalParamType);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener menu por Id " + ex.Message);
            }
        }
        public async Task<ResultSp> CreateGeneralParameterAsync(GeneralParameterInput generalParam)
        {
            try
            {
                var generalParamType = _mapper.Map<GeneralParameterType>(generalParam);
                var result = await _generalParameterCommands.CreateGeneralParamAsync(generalParamType);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear parametro general " + ex.Message);
            }
        }

        public async Task<ResultSp> UpdateGeneralParameterAsync(GeneralParameterInput generalParam)
        {
            try
            {
                var generalParamType = _mapper.Map<GeneralParameterType>(generalParam);
                var result = await _generalParameterCommands.UpdateGeneralParamAsync(generalParamType);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al acrtualizar parametro general " + ex.Message);
            }
        }
    }
}
