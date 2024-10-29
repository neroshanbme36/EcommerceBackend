using Application.QueryParams.Pagination;

namespace Application.QueryParams
{
    public class ProductParams : BasePaginationParams
    {
        public string? Description {get; set;}
        public string? Category { get; set; }
        public string? Brand { get; set; }
        public string? Attribute { get; set; }
        public string? Price { get; set; }
        public string? Rating { get; set; }
        public string? Availability {get; set;}
    }
}