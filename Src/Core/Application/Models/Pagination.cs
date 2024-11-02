namespace Application.Models
{
    public class Pagination<T> 
    {
        public int PageNumber {get; set;}
        public int PageSize { get; set; }  
        public int TotalPages { get; set; } 
        public long TotalCount { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageNumber, int pageSize, long totalCount, IReadOnlyList<T> data)
        {
            PageNumber = pageNumber;
            if (PageNumber < 1) PageNumber = 1;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalCount/ (double)pageSize);
            TotalCount = totalCount;
            Data = data;
        }
    }
}