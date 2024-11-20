using Application.Contracts.Features;
using Application.Contracts.Persistence;
using Application.Dtos.CloudStoreEpos.Epos;
using Domain.Entities;
using Domain.Enums.CloudStoreEpos;

namespace Application.Features
{
    public class MediaFileService : IMediaFileService
    {
        private readonly IUnitOfWork _storeUnitOfWork;

        public MediaFileService(IUnitOfWork storeUnitOfWork)
        {
            _storeUnitOfWork = storeUnitOfWork;
        }

        public async Task<OrderDto> MapMediaFileToOrder(OrderDto order)
        {
            IReadOnlyList<OrderDto> orders = new List<OrderDto>{order};
            orders = await MapMediaFilesToOrders(orders);
            return orders[0];
        }
        
        public async Task<IReadOnlyList<OrderDto>> MapMediaFilesToOrders(IReadOnlyList<OrderDto> orderDtos)
        {
            List<string> productItemNos = new List<string>();
            foreach (OrderDto order in orderDtos)
            {
                foreach (ParentProdLineOrderDto parentProdLineOrder in order.ParentProductLines)
                {
                    if (parentProdLineOrder.EntryType == EntryType.Product)
                    {
                        bool isItemNoExits = productItemNos.Any(c => c == parentProdLineOrder.KeyId);
                        if (!isItemNoExits) productItemNos.Add(parentProdLineOrder.KeyId);
                    }
                }
            }
            if (productItemNos.Count > 0)
            {
                IReadOnlyList<string> mediaFileTypes = new List<string> { "Thumbnail" };
                IReadOnlyList<MediaFile> mediaFiles = await _storeUnitOfWork.MediaFileRepository.GetMediaFilesByEntityTypeEntityIdsType("Products", mediaFileTypes, productItemNos);
                if (mediaFiles.Count > 0)
                {
                    foreach (OrderDto order in orderDtos)
                    {
                        foreach (ParentProdLineOrderDto parentProdLineOrder in order.ParentProductLines)
                        {
                            if (parentProdLineOrder.EntryType == EntryType.Product)
                            {
                                MediaFile? prodMediaFile = mediaFiles.FirstOrDefault(c => c.EntityId == parentProdLineOrder.KeyId);
                                if (prodMediaFile != null) parentProdLineOrder.ThumbnailUrl = prodMediaFile.Url;
                            }
                        }
                    }
                }
            }
            return orderDtos;
        }
    }
}