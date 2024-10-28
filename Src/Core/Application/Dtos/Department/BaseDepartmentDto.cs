namespace Application.Dtos.Department
{
    public class BaseDepartmentDto
    {
        public string Id { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool ShowEcommHome {get; set;}
        public bool ShowEcommFooter {get; set;}
        public string? ImageUrl { get; set; }
    }
}