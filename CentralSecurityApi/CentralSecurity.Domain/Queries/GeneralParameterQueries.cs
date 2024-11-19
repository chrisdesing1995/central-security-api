using AutoMapper;
using CentralSecurity.Domain.Dto;
using CentralSecurity.Domain.Interfaces.Repositories;
using CentralSecurity.Domain.Queries.Interfaces;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Queries
{
    public class GeneralParameterQueries: IGeneralParameterQueries
    {
        private readonly IMapper _mapper;
        private readonly IGeneralParameterRepository _generalParameterRepository;

        public GeneralParameterQueries(IMapper mapper, IGeneralParameterRepository generalParameterRepository)
        {
            _mapper = mapper;
            _generalParameterRepository = generalParameterRepository;
        }

        public async Task<IEnumerable<GeneralParameterType>> GetAllGeneralParameterAsync()
        {
            try
            {
                var generalParams = await _generalParameterRepository.GetAllGeneralParameterAsync();

                return _mapper.Map<IEnumerable<GeneralParameterType>>(generalParams);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista los parametros generales " + ex.Message);
            }
        }

        public async Task<GeneralParameterType> GetGeneralParameterByIdAsync(Guid generalParamId)
        {
            try
            {
                var parameters = await _generalParameterRepository.GetGeneralParameterByIdAsync(generalParamId);
                parameters.Details = (List<GeneralParameterDetailSpDto>)await _generalParameterRepository.GetGeneralParameterByCodeAsync(parameters.Code);

                return _mapper.Map<GeneralParameterType>(parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener parametros por Id " + ex.Message);
            }
        }

        public async Task<IEnumerable<GeneralParameterDetailType>> GetGeneralParameterByCodeAsync(string code)
        {
            try
            {
                var parameters = await _generalParameterRepository.GetGeneralParameterByCodeAsync(code);

                return _mapper.Map<IEnumerable<GeneralParameterDetailType>>(parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener parametros por codigo cabecera " + ex.Message);
            }
        }
    }
}
