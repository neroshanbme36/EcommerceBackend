using Domain.Enums.CloudStoreEpos;

namespace Application.Dtos.CloudStoreEpos.Epos
{
    public class EposTransLineInputDto
    {
        public int LineNo {get; set;}
        public long HeaderId {get; set;}
        public EntryType EntryType {get; set;} 
        public TransactionType TransType {get; set;} 
        public bool LineStatus {get; set;}
        public string? ItemNo { get; set; } // Priority 2
        public string? Barcode {get; set;} // Priority 1
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Scanned {get; set;}
        public string? FreeText {get; set;}
        public bool InfoCodeAuthorized {get; set;}
        public string? RepairId {get; set;}
        public string? Guid {get; set;}
    }
}