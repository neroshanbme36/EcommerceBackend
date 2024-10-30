using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ItemNo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Barcode { get; set; }
        public bool NumericeBarcode { get; set; }
        public bool PriceEntry { get; set; }
        public bool QuantityEntry { get; set; }
        public decimal Price { get; set; }
        public decimal TakeawayPrice { get; set; }
        public decimal DeliveryPrice { get; set; }
        public string? VendorItemNo { get; set; }
        public string? UnitSize { get; set; } 
        public decimal Rrp { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Margin { get; set; }
        public string? OuterEan { get; set; }
        public bool NumericOuterEan { get; set; }
        public string? Pum { get; set; }
        public decimal PackCost { get; set; }
        public decimal PackQuantity { get; set; }
        public string? PackDescription { get; set; }
        public bool ManageStock { get; set; }
        public decimal StockQuantity { get; set; }
        public decimal ReorderPoint { get; set; }
        public decimal ReorderQuantity { get; set; }
        public decimal MaxOrderQuantity { get; set; } 
        public bool Scale { get; set; }
        public bool DiscountAllowed { get; set; }
        public bool NonDiscountable { get; set; }
        public string? ParentItemNo { get; set; } 
        public bool IsKot { get; set; }
        public DateTime LastSalesOn { get; set; }
        public DateTime LastSalesOnGmt { get; set; } 
        public DateTime CreatedOn { get; set; } 
        public DateTime CreatedOnGmt { get; set; } 
        public DateTime ModifiedOn { get; set; }
        public DateTime ModifiedOnGmt { get; set; } 
        public bool IsActive { get; set; }
        public string? QuickProductId { get; set; } //
        public string? Brand {get; set;} 
        public string? ShelfNo {get; set;}
        public string? ShelfStaffId {get; set;} 
        public string? ShelfRow {get; set;} 
        public string? ShelfColumn {get; set;}
        public string? FullDescription {get; set;} // short description
        public bool Favourite {get; set;}
        public string? LongDescription {get; set;}
        public string Type {get; set;} = string.Empty;
        public bool IsFeatured {get; set;}
        public bool IsTrending {get; set;}
        public bool IsTopSelling {get; set;}
        public bool IsTodayDeal {get; set;}
        public bool IsRecentlyAdded {get; set;}
        public bool ShowInEcommerce {get; set;}
        public decimal OriginalPrice {get; set;}
        public decimal DiscPercentage {get; set;}
        
        public string DepartmentId { get; set; } = string.Empty;
        public Department? Department { get; set; }

        public string VatId { get; set; } = string.Empty;
        public Vat? Vat { get; set; }

        public string? ProductGroupId { get; set; }
        public ProductGroup? ProductGroup { get; set; }

        public string? ProductSubGroupId { get; set; }
        public ProductSubGroup? ProductSubGroup { get; set; }

        public string? VendorId { get; set; }
        public Vendor? Vendor { get; set; }

        public IReadOnlyList<ProductBarcode> ProductBarcodes { get; set; } = new List<ProductBarcode>();
        public BarcodeMask? BarcodeMask { get; set; }
        public IReadOnlyList<ProductAttributeValue> ProductAttributeValues {get; set;} = new List<ProductAttributeValue>();
        public IReadOnlyList<ProductDepartment> ProductDepartments {get; set;} = new List<ProductDepartment>();
        public IReadOnlyList<ProductTag> ProductTags {get; set;} = new List<ProductTag>();
        public IReadOnlyList<WishlistProduct> WishlistProducts {get; set;} = new List<WishlistProduct>();
    }
}