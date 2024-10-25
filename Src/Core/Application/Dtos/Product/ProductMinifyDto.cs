namespace Application.Dtos.Product
{
    public class ProductMinifyDto
    {
        public string ItemNo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? UnitSize { get; set; }
        public string? Pum { get; set; }
        public bool ManageStock { get; set; }
        public decimal StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public string? Brand { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public decimal DiscPercentage { get; set; }
        public string? ThumbnailUrl {get; set;}
    }
}