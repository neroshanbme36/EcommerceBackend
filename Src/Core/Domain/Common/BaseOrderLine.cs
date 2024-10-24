using System;
using Domain.Enums.CloudStoreEpos;

namespace Domain.Common
{
    public class BaseOrderLine : BaseEntity
    {
        public EntryType EntryType { get; set; }
        public TransactionType TransType { get; set; }
        public int LineNo { get; set; }
        public bool LineStatus { get; set; }
        public string Barcode { get; set; }
        public string KeyId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal VatAmount { get; set; }
        public string VatId { get; set; }
        public decimal VatRate { get; set; }
        public string DepartmentId { get; set; }
        public string ProductGroupId { get; set; }
        public string ProductSubGroupId { get; set; }
        public bool IsPriceMark { get; set; }
        public bool Discountable { get; set; }
        public bool IsOffer { get; set; }
        public string OfferId { get; set; }
        public decimal OfferQuantity { get; set; }
        public bool Scanned { get; set; }
        public string MbId { get; set; }
        public bool Mandatory { get; set; }
        public bool Scale { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
        public int LinkedOfferId { get; set; }
        public bool IsModifier { get; set; }
        public int ModifierHeaderId { get; set; }
        public string ModifierTitle { get; set; }
        public string ParentKeyId { get; set; }
        public int ParentLineNo { get; set; }
        public int PrinterId { get; set; }
        public bool IsKot { get; set; }
        public bool IsKotPrinted { get; set; }
        public DateTime KotPrintedOn { get; set; }
        public bool IsServed { get; set; }
        public string FreeText { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime CreatedOnGmt { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime ModifiedOnGmt { get; set; }
        public string DeviceId {get; set;}

        public long HeaderId { get; set; }
    }
}