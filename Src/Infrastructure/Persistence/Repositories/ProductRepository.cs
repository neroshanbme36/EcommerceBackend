using Application.Contracts.Persistence;
using Application.Helpers;
using Application.Models;
using Application.QueryParams;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Helpers;

namespace Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly MainDbContext _dbContext;

        public ProductRepository(MainDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Product>> GetProductHighlights()
        {
            return await _dbContext.Products
                .Where(c => c.ShowInEcommerce &&
                (c.IsFeatured || c.IsTrending || c.IsTopSelling
                || c.IsTodayDeal || c.IsRecentlyAdded))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductsByItemNos(IReadOnlyList<string> itemNos)
        {
            return await _dbContext.Products
                .Where(c => c.ShowInEcommerce &&
                itemNos.Contains(c.ItemNo))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductsWithManageStockByItemNos(IReadOnlyList<string> itemNos)
        {
            return await _dbContext.Products
                .Where(c => c.ShowInEcommerce &&
                itemNos.Contains(c.ItemNo)
                && c.ManageStock)
                .ToListAsync();
        }

        public async Task<Product?> GetProductDetailByDescription(string description)
        {
            return await _dbContext.Products
                .Include(c => c.ProductDepartments).ThenInclude(c => c.Department)
                .Include(c => c.ProductTags).ThenInclude(c => c.Tag)
                .FirstOrDefaultAsync(c => c.ShowInEcommerce && c.Description == description);
        }

        public async Task<IReadOnlyList<Product>> GetWishlistProductsByUserId(string userId)
        {
            return await _dbContext.WishlistProducts
                .Include(c => c.Product)
                .Where(c => c.Product.ShowInEcommerce && c.UserId == userId)
                .Select(c => c.Product)
                .ToListAsync();
        }

        public async Task<bool> IsAnyByItemNo(string itemNo)
        {
            return await _dbContext.Products
                .AnyAsync(c => c.ShowInEcommerce && c.ItemNo == itemNo);
        }

        public async Task<IReadOnlyList<Product>> GetRelatedProducts(string itemNo)
        {
            IQueryable<string> quRelatedProdsItemNos = _dbContext.RelatedProducts
                .Where(c => c.ItemNo == itemNo).Select(c => c.RelatedProductItemNo);

            return await _dbContext.Products
                .Where(c => c.ShowInEcommerce 
                && quRelatedProdsItemNos.Contains(c.ItemNo))
                .ToListAsync();
        }

        public async Task<Pagination<Product>> GetProducts(ProductParams productParams)
        {
            IReadOnlyList<string> categories = string.IsNullOrWhiteSpace(productParams.Category) ? new List<string>() : productParams.Category.Split(',').ToList();
            IReadOnlyList<string> brands = string.IsNullOrWhiteSpace(productParams.Brand) ? new List<string>() : productParams.Brand.Split(',').ToList();
            IReadOnlyList<string> prices = string.IsNullOrWhiteSpace(productParams.Price) ? new List<string>() : productParams.Price.Split(',').ToList();
            IReadOnlyList<int> ratings = string.IsNullOrWhiteSpace(productParams.Rating) ? new List<int>() : productParams.Rating.Split(',').Select(int.Parse).ToList();
            IReadOnlyList<string> attributes = string.IsNullOrWhiteSpace(productParams.Attribute) ? new List<string>() : productParams.Attribute.Split(',').ToList();

            IQueryable<Product> quProducts = _dbContext.Products.Where(c => c.ShowInEcommerce);

            if (!string.IsNullOrWhiteSpace(productParams.Description))
                quProducts = quProducts.Where(c => c.Description.Contains(productParams.Description));

            if (!string.IsNullOrWhiteSpace(productParams.Availability))
            {
                if (string.Equals("In Stock", productParams.Availability, StringComparison.OrdinalIgnoreCase))
                    quProducts = quProducts.Where(c => c.StockQuantity > 0.00m);
                else if (string.Equals("Out Of Stock", productParams.Availability, StringComparison.OrdinalIgnoreCase))
                    quProducts = quProducts.Where(c => c.StockQuantity <= 0.00m);
            }

            if (prices.Count > 0)
            {
                IReadOnlyList<string> priceRange = prices[0].Split('-').ToList();
                decimal minPrice = MathHelper.IsNumeric(priceRange[0]) ? Convert.ToDecimal(priceRange[0]) : 0.00m;
                decimal maxPrice = MathHelper.IsNumeric(priceRange[1]) ? Convert.ToDecimal(priceRange[1]) : 0.00m;
                quProducts = quProducts.Where(c => c.Price >= minPrice && c.Price <= maxPrice);
            }

            if (categories.Count > 0)
                quProducts = quProducts.Where(c => c.ProductDepartments.Any(c => categories.Contains(c.DepartmentId)));

            if (brands.Count > 0)
                quProducts = quProducts.Where(c => brands.Contains(c.Brand));

            if (ratings.Count > 0)
            {
                // need to implement
            }

            if (attributes.Count > 0)
            {
                // need to implement
            }

            if (!string.IsNullOrWhiteSpace(productParams.Sort))
            {
                if (string.Equals("CreatedOnAsc", productParams.Sort, StringComparison.OrdinalIgnoreCase))
                    quProducts = quProducts.OrderBy(c => c.CreatedOn);
                else if (string.Equals("CreatedOnDesc", productParams.Sort, StringComparison.OrdinalIgnoreCase))
                    quProducts = quProducts.OrderByDescending(c => c.CreatedOn);
                else if (string.Equals("A-Z", productParams.Sort, StringComparison.OrdinalIgnoreCase))
                    quProducts = quProducts.OrderBy(c => c.Description);
                else if (string.Equals("Z-A", productParams.Sort, StringComparison.OrdinalIgnoreCase))
                    quProducts = quProducts.OrderByDescending(c => c.Description);
                else if (string.Equals("Low-High", productParams.Sort, StringComparison.OrdinalIgnoreCase))
                    quProducts = quProducts.OrderBy(c => c.Price);
                else if (string.Equals("High-Low", productParams.Sort, StringComparison.OrdinalIgnoreCase))
                    quProducts = quProducts.OrderByDescending(c => c.Price);
            }

            return await PagedListHelper<Product>.CreateAsyncWithoutRenderLastPage(quProducts, productParams.PageNumber, productParams.PageSize);
        }
    }
}