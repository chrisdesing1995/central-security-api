using CentralSecurity.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralSecurity.Domain.Interfaces.Repositories
{
    public interface IObjectFileRepository
    {
        Task<IEnumerable<ObjectFileSpDto>> GetAllObjectFileByEntityAsync(Guid? entityId, string? entityName);
        Task<ObjectFileSpDto> GetObjectFileByEntityAsync(Guid? entityId, string? entityName);
    }
}
