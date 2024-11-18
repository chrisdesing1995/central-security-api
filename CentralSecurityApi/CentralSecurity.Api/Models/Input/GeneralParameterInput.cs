
namespace CentralSecurity.Api.Models.Input
{
    public class GeneralParameterInput
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; } = "A";
        public List<GeneralParameterDetailInput> Details { get; set; } = new List<GeneralParameterDetailInput>();
    }

    public class GeneralParameterDetailInput
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Value1 { get; set; }
        public string? Value2 { get; set; }
        public string? Value3 { get; set; }
        public string? Value4 { get; set; }
        public string? Value5 { get; set; }
    }
}
