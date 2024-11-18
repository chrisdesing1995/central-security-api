using CentralSecurity.Domain.Common;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Commands.Interfaces
{
    public interface IGeneralParameterCommands
    {
        Task<ResultSp> CreateGeneralParamAsync(GeneralParameterType generalParameterTypeType);
        Task<ResultSp> UpdateGeneralParamAsync(GeneralParameterType generalParameterTypeType);
    }
}
