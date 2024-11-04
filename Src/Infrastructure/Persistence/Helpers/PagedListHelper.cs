using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.Helpers
{
    public class PagedListHelper<T>
    {
        public static Pagination<T> CreateSync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            long count = source.Count();

            int tPages = (int)Math.Ceiling(count/ (double)pageSize);
            if (pageNumber > tPages)
            {
                pageNumber = tPages - 1; // render last page
            }

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new Pagination<T>(pageNumber,pageSize, count, items);
        }

        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            long count = await source.CountAsync();

            int tPages = (int)Math.Ceiling(count/ (double)pageSize);
            if (pageNumber > tPages)
            {
                pageNumber = tPages - 1; // render last page
            }
            var items = new List<T>();
            if(count > 0)
            {
                items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            
            return new Pagination<T>(pageNumber,pageSize, count, items);
        }

        public static async Task<Pagination<T>> CreateAsyncWithoutRenderLastPage(IQueryable<T> source, int pageNumber, int pageSize)
        {
            long count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Pagination<T>(pageNumber,pageSize, count, items);
        }
    }
}