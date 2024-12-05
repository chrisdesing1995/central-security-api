using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralSecurity.Domain.Dto
{
    public class ObjectFileDto : AuditableEntityDto
    {
        public Guid? EntityId { get; set; }
        public string? EntityName { get; set; }
        public string? ObjectData { get; set; }
    }
}
