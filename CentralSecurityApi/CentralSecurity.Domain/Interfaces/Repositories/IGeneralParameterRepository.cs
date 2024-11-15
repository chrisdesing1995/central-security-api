
using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Dto;

namespace CentralSecurity.Domain.Interfaces.Repositories
{
    public interface IGeneralParameterRepository
    {
        Task<IEnumerable<GeneralParameterSpDto>> GetAllMenuAsync();
        Task<GeneralParameterSpDto> GetMenuByIdAsync(Guid id);
        Task<IEnumerable<GeneralParameterDetailSpDto>> GetMenuByCodeAsync(string code);
        Task<ResultSp> CreateMenuAsync(GeneralParameterDto generalParameter);
        Task<ResultSp> UpdateMenuAsync(GeneralParameterDto generalParameter);
        Task<ResultSp> DeleteMenuAsync(Guid id);
    }
}
