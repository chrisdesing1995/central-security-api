
namespace CentralSecurity.Domain.Common
{
    public interface IAuditService
    {
        string GetCurrentUserId();
        string GetCurrentUserName();
    }
}
