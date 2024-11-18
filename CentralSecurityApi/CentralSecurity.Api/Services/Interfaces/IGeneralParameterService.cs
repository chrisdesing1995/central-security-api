using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Domain.Common;

namespace CentralSecurity.Api.Services.Interfaces
{
    public interface IGeneralParameterService
    {
        Task<IEnumerable<GeneralParameterOutput>> GetAllGeneralParameterAsync();
        Task<GeneralParameterOutput> GetGeneralParameterByIdAsync(Guid generalParamId);
        Task<IEnumerable<GeneralParameterOutput>> GetGeneralParameterByCodeAsync(string code);
        Task<ResultSp> CreateGeneralParameterAsync(GeneralParameterInput generalParam);
        Task<ResultSp> UpdateGeneralParameterAsync(GeneralParameterInput generalParam);
    }
}
