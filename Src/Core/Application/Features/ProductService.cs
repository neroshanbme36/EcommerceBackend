using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.Department;
using Application.Dtos.Product;
using Application.Dtos.Tag;
using Application.Exceptions;
using Application.Models;
using Application.QueryParams;
using AutoMapper;
using Domain.Entities;

namespace Application.Features
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IDepartmentService departmentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _departmentService = departmentService;
        }

        public async Task<ProductHightlightsDto> GetProductHighlights()
        {
            ProductHightlightsDto productHightlightsDto = new ProductHightlightsDto();
            IReadOnlyList<Product> productHightlightCollections = await _unitOfWork.ProductRepository.GetProductHighlights();
            IReadOnlyList<ProductDto> productDtos = await ConvertToProductDto(productHightlightCollections);
            foreach (ProductDto productDto in productDtos)
            {
                ProductMinifyDto productMinifyDto = _mapper.Map<ProductMinifyDto>(productDto);
                if (productDto.IsFeatured) productHightlightsDto.FeaturedProducts.Add(productMinifyDto);
                if (productDto.IsTrending) productHightlightsDto.TrendingProducts.Add(productMinifyDto);
                if (productDto.IsTopSelling) productHightlightsDto.TopSellingProducts.Add(productMinifyDto);
                if (productDto.IsTodayDeal) productHightlightsDto.TodayDealProducts.Add(productMinifyDto);
                if (productDto.IsRecentlyAdded) productHightlightsDto.RecentlyAddedProducts.Add(productMinifyDto);
            }
            return productHightlightsDto;
        }

        public async Task<IReadOnlyList<DepartmentProductMinifyDto>> GetHomepageDepartmentProducts()
        {
            List<DepartmentProductMinifyDto> departmentProducts = new List<DepartmentProductMinifyDto>();
            IReadOnlyList<DepartmentDto> homepageDeptDtos = await _departmentService.GetHomePageDepartments();
            if (homepageDeptDtos.Count > 0)
            {
                IReadOnlyList<string> departmentIds = homepageDeptDtos.Select(c => c.Id).ToList();

                IReadOnlyList<ProductDepartment> productDepartments = await _unitOfWork.ProductDepartmentRepository.GetProductDepartmentsByDeptIds(departmentIds);
                if (productDepartments.Count > 0)
                {
                    IReadOnlyList<string> prodDeptItemNos = productDepartments.Select(c => c.ItemNo).ToList();
                    IReadOnlyList<Product> homepageProds = await _unitOfWork.ProductRepository.GetProductsByItemNos(prodDeptItemNos);
                    IReadOnlyList<ProductDto> homepageProductDtos = await ConvertToProductDto(homepageProds);
                    foreach (DepartmentDto deptDto in homepageDeptDtos)
                    {
                        IReadOnlyList<string> deptProdItemNos = productDepartments
                            .Where(c => c.DepartmentId == deptDto.Id)
                            .Select(c => c.ItemNo)
                            .ToList();

                        if (deptProdItemNos.Count > 0)
                        {
                            IReadOnlyList<ProductDto> productDtos = homepageProductDtos.Where(c => deptProdItemNos.Contains(c.ItemNo)).ToList();
                            DepartmentProductMinifyDto departmentProductMinifyDto = _mapper.Map<DepartmentProductMinifyDto>(deptDto);
                            departmentProductMinifyDto.Products = _mapper.Map<IReadOnlyList<ProductMinifyDto>>(productDtos);
                            departmentProducts.Add(departmentProductMinifyDto);
                        }
                    }
                }
            }
            return departmentProducts;
        }

        public async Task<Pagination<ProductMinifyDto>> GetProducts(ProductParams productParams)
        {
            Pagination<Product> paginationProds = await _unitOfWork.ProductRepository.GetProducts(productParams);
            IReadOnlyList<ProductDto> productDtos = await ConvertToProductDto(paginationProds.Data);
            IReadOnlyList<ProductMinifyDto> productMinifyDtos = _mapper.Map<IReadOnlyList<ProductMinifyDto>>(productDtos);
            return new Pagination<ProductMinifyDto>(productMinifyDtos, paginationProds.TotalCount, paginationProds.PageNumber, paginationProds.PageSize);
        }

        public async Task<ProductDetailDto> GetProductBySlug(string slug)
        {
            string description = slug.Replace("-", " ");

            Product? product = await _unitOfWork.ProductRepository.GetProductDetailByDescription(description);
            if (product == null) throw new NotFoundException("Product doesnt exist", slug);

            ProductDetailDto productDetailDto = _mapper.Map<ProductDetailDto>(product);
            await BindMediaFiles(productDetailDto);
            if (product.ProductTags.Count > 0)
            {
                IReadOnlyList<Tag> tags = product.ProductTags.Select(c => c.Tag).ToList();
                productDetailDto.Tags = _mapper.Map<IReadOnlyList<TagDto>>(tags);
            }
            await BindRelatedProducts(productDetailDto);
            return productDetailDto;
        }

        private async Task BindRelatedProducts(ProductDetailDto productDetailDto)
        {
            IReadOnlyList<Product> relatedProducts = await _unitOfWork.ProductRepository.GetRelatedProducts(productDetailDto.ItemNo);
            IReadOnlyList<ProductDto> productDtos = await ConvertToProductDto(relatedProducts);
            productDetailDto.RelatedProducts = _mapper.Map<IReadOnlyList<ProductMinifyDto>>(productDtos);
        }

        private async Task BindMediaFiles(ProductDetailDto productDetailDto)
        {
            IReadOnlyList<string> productItemNos = new List<string> { productDetailDto.ItemNo };
            IReadOnlyList<string> mediaFileTypes = new List<string> { "Thumbnail", "Image" };
            IReadOnlyList<MediaFile> mediaFiles = await _unitOfWork.MediaFileRepository.GetMediaFilesByEntityTypeEntityIdsType("Products", mediaFileTypes, productItemNos);
            foreach (MediaFile mediaFile in mediaFiles)
            {
                if (mediaFile.Type.Equals("Image"))
                    productDetailDto.ImageUrls.Add(mediaFile.Url);
                else
                    productDetailDto.ThumbnailUrl = mediaFile.Url;
            }
        }

        private async Task<IReadOnlyList<ProductDto>> ConvertToProductDto(IReadOnlyList<Product> products)
        {
            IReadOnlyList<ProductDto> productDtos = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            if (products.Count > 0)
            {
                IReadOnlyList<string> productItemNos = productDtos.Select(c => c.ItemNo).ToList();
                IReadOnlyList<string> mediaFileTypes = new List<string> { "Thumbnail", "Image" };
                IReadOnlyList<MediaFile> mediaFiles = await _unitOfWork.MediaFileRepository.GetMediaFilesByEntityTypeEntityIdsType("Products", mediaFileTypes, productItemNos);
                if (mediaFiles.Count > 0)
                {
                    foreach (ProductDto prodDto in productDtos)
                    {
                        IReadOnlyList<MediaFile> prodMediaFiles = mediaFiles.Where(c => c.EntityId == prodDto.ItemNo).ToList();
                        foreach (MediaFile prodMediaFile in prodMediaFiles)
                        {
                            if (prodMediaFile.Type.Equals("Image"))
                                prodDto.ImageUrls.Add(prodMediaFile.Url);
                            else
                                prodDto.ThumbnailUrl = prodMediaFile.Url;
                        }
                    }
                }
            }
            return productDtos;
        }
    }
}