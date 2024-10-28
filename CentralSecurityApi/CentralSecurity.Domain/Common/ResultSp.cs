
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurity.Domain.Common
{
    public class ResultSp
    {
        [Column("Status")]
        public bool Status { get; set; }
        [Column("Messages")]
        public string Message { get; set; }
        [Column("Data")]
        public string? Data { get; set; }
    }
}
