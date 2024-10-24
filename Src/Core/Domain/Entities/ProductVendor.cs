using Domain.Common;

namespace Domain.Entities
{
    public class ProductVendor : BaseEntity
    {
        public string ItemNo { get; set; } = string.Empty; // PK 20
        public string VendorId { get; set; } = string.Empty; // PK 20
        public string VendorItemNo { get; set; } = string.Empty; // PK 20
        public string Description { get; set; } = string.Empty; // 50
        public decimal PackQuantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Rrp { get; set; }
        public decimal Profit { get; set; } 
        public string? OBarcode { get; set; } //20
        public string? BlackListed { get; set; } //10
        public int MaxOrderQuantity { get; set; }
        public string? PriceType { get; set; } // 2
    }
}