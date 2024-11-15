namespace CentralSecurity.Domain.Entities
{
    public class GeneralParameter: AuditableEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; } = "A";

        public List<GeneralParameterDetail> Details { get; set; } = new List<GeneralParameterDetail>();
    }

    public class GeneralParameterDetail: AuditableEntity
    {
        public Guid GeneralParameterId { get; set; }
        public string Code { get; set; }
        public string Value1 { get; set; }
        public string? Value2 { get; set; }
        public string? Value3 { get; set; }
        public string? Value4 { get; set; }
        public string? Value5 { get; set; }
     
    }
}
