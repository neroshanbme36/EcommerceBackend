using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.WishlistProduct;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;

namespace Application.Features
{
    public class WishlistProductService : IWishlistProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public WishlistProductService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<WishlistProductDto>> GetWishlistProductsByUserId(string userId)
        {
            IReadOnlyList<WishlistProduct> wishlistProducts = await _uow.WishlistProductRepository.GetWishProductsByUserId(userId);
            return _mapper.Map<IReadOnlyList<WishlistProductDto>>(wishlistProducts);
        }

        public async Task AddWishlistProduct(string userId, AddWistlistProductDto request)
        {
            bool isAny = await _uow.WishlistProductRepository.IsAny(userId, request.ItemNo);
            if (isAny) throw new BadRequestException("Wishlist product already exist with this user");

            bool isItemExist = await _uow.ProductRepository.IsAnyByItemNo(request.ItemNo);
            if (!isItemExist) throw new BadRequestException("Product doesnt exist");

            WishlistProduct wishlistProduct = _mapper.Map<WishlistProduct>(request);
            wishlistProduct.UserId = userId;
            wishlistProduct.CreatedOn = DateTime.Now;

            await _uow.WishlistProductRepository.Add(wishlistProduct);
            bool isSaved = await _uow.Save() > 0;
            if (!isSaved) throw new InternalServerErrorException("Wishlist product save failed");
        }

        public async Task DeleteWishlistProduct(string userId, string itemNo)
        {
            WishlistProduct? wishlistProduct = await _uow.WishlistProductRepository.GetWishlistProductByUserIdAndItemNo(userId, itemNo);
            if (wishlistProduct == null) throw new NotFoundException("Wishlist product doesnt exist", itemNo);

            await _uow.WishlistProductRepository.Delete(wishlistProduct);
            bool isSaved = await _uow.Save() > 0;
            if (!isSaved) throw new InternalServerErrorException("Wishlist product delete failed");
        }
    }
}