
namespace CentralSecurity.Domain.Dto
{
    public class GeneralParameterSpDto
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; } = "A";
        public List<GeneralParameterDetailSpDto> Details { get; set; } = new List<GeneralParameterDetailSpDto>();
    }

    public class GeneralParameterDetailSpDto
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
