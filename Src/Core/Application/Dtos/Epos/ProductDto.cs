
namespace Application.Dtos.CloudStoreEpos.Epos
{
    public class ProductDto
    {
        public string ItemNo { get; set; }
        public string? Barcode { get; set; }
        public string Description { get; set; } = string.Empty;
        public string VatId { get; set; }  = string.Empty;
        public decimal VatRate { get; set; }
        public string DepartmentId { get; set; }  = string.Empty;
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
    }
}