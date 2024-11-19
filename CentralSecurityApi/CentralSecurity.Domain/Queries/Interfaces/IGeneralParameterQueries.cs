using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Queries.Interfaces
{
    public interface IGeneralParameterQueries
    {
        Task<IEnumerable<GeneralParameterType>> GetAllGeneralParameterAsync();
        Task<GeneralParameterType> GetGeneralParameterByIdAsync(Guid generalParamId);
        Task<IEnumerable<GeneralParameterDetailType>> GetGeneralParameterByCodeAsync(string code);
    }
}
