using System;
using Domain.Common;

namespace Domain.Entities
{
    public class ProductBarcode : BaseEntity
    {
        public string Barcode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool NumericeBarcode { get; set; }
        public bool MarkedPrice { get; set; } //
        public decimal Price { get; set; }
        public decimal TakeawayPrice { get; set; }
        public decimal DeliveryPrice { get; set; }
        public string? UnitSize { get; set; }
        public string? Colour { get; set; }
        public decimal Rrp { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Margin { get; set; }
        public decimal PackCost { get; set; }
        public decimal PackQuantity { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime CreatedOnGmt { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime ModifiedOnGmt { get; set; }

        public string ItemNo { get; set; } = string.Empty;
        public Product? Product { get; set; }
    }
}