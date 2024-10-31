using CentralSecurity.Api.Models.Input;
using CentralSecurity.Api.Models.Output;
using CentralSecurity.Domain.Types;

namespace CentralSecurity.Api.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginOutput> AuthenticateLogin(LoginInput input);
    }
}
