using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;


namespace CentralSecurity.Domain.Common
{
    public class AuditService : IAuditService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;

        public AuditService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }

        public string GetCurrentUserName()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            return userName;
        }
    }
}
