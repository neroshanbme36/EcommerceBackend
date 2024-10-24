using System.Collections.Generic;
using Application.Enums.CloudStoreEpos;

namespace Application.Dtos.CloudStoreEpos.Epos
{
    public class ProductSearchResultDto
    {
        public ProductSearchResultCode Code {get; set;}
        public string? Message {get; set;}
        public ProductDto QuickProduct {get; set;}
        public InfoCodeTransactionDto InfoCodeTransaction {get; set;}
        public OrderDto Order {get; set;}
        public IReadOnlyList<PromotionHeaderDto> PromotionHeaders {get; set;}
    }
}