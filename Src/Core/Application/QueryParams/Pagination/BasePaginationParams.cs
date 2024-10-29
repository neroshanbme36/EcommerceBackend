namespace Application.QueryParams.Pagination
{
    public class BasePaginationParams
    {
        private const int MaxPageSize = 100;
        public int PageNumber { get; set; } = 1;

        private int pageSize = 6;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Sort { get; set; } // asc, desc, low-high, high-low, a-z,z-a
    }
}