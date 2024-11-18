
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;

namespace CentralSecurity.Domain.Interfaces.Repositories
{
    public interface IGeneralParameterRepository
    {
        Task<IEnumerable<GeneralParameterSpDto>> GetAllGeneralParameterAsync();
        Task<GeneralParameterSpDto> GetGeneralParameterByIdAsync(Guid id);
        Task<IEnumerable<GeneralParameterDetailSpDto>> GetGeneralParameterByCodeAsync(string code);
        Task<ResultSp> CreateGeneralParameterAsync(GeneralParameterDto generalParameter);
        Task<ResultSp> UpdateGeneralParameterAsync(GeneralParameterDto generalParameter);
        Task<ResultSp> DeleteGeneralParameterAsync(Guid id);
    }
}
