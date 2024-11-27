
namespace CentralSecurity.Domain.Dto
{
    public class MenuSpDto
    {
        public Guid? Id { get; set; }
        public string MenuName { get; set; }
        public Guid? ParentId { get; set; }
        public string? ParentName { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int SortOrder { get; set; }
        public string IsActive { get; set; }
    }
}
