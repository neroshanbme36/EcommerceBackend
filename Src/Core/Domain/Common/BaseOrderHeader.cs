using System;
using Domain.Enums.CloudStoreEpos;

namespace Domain.Common
{
  public class BaseOrderHeader : BaseEntity
  {
    public long Id { get; set; }
    public string StoreId { get; set; }
    public string DeviceId { get; set; }
    public string UserId { get; set; }
    public string StaffName {get; set;}
    public decimal SalesAmount {get; set;}
    public TransactionType TransType { get; set; }
    public OrderType OrderType { get; set; }
    public long TakeawayId { get; set; }
    public long DeliveryId { get; set; }
    public bool IsScheduledOrder { get; set; }
    public DateTime RequestedOn { get; set; }
    public string LocationId { get; set; }
    public string LocationDesc { get; set; }
    public int TableId { get; set; }
    public int Seats { get; set; }
    public int InfoItem { get; set; }
    public string FreeText { get; set; }
    public string CustomerId { get; set; }
    public string LoyaltyCardNo { get; set; }
    public string Provider { get; set; }
    public string MerchantReceiptNo { get; set; }
    public bool IsMerchantPrinted { get; set; }
    public string CustomerRecepitNo { get; set; }
    public bool IsCustomerPrinted { get; set; }
    public bool CustPrint { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime CreatedOnGmt { get; set; }
    public string? RepairId {get; set;}
    public bool IsVoided {get; set;}
    public string? EcommCustomerId {get; set;}
    public string? CustFirstName { get; set; }
    public string? CustLastName { get; set; }
    public string? CustPhone {get; set;}
    public string? CustEmail {get; set;}
    public long BillCustAddressId {get; set;}
    public string? BillAddressLine1 { get; set; }
    public string? BillAddressLine2 { get; set; }
    public string? BillAddressLine3 { get; set; }
    public string? BillAddressLine4 { get; set; }
    public string? BillCity { get; set; }
    public string? BillState { get; set; }
    public string? BillPostcode { get; set; }
    public string? BillCountryId { get; set; }
    public long DeliCustAddressId {get; set;}
    public string? DeliAddressLine1 { get; set; }
    public string? DeliAddressLine2 { get; set; }
    public string? DeliAddressLine3 { get; set; }
    public string? DeliAddressLine4 { get; set; }
    public string? DeliCity { get; set; }
    public string? DeliState { get; set; }
    public string? DeliPostcode { get; set; }
    public string? DeliCountryId { get; set; }
    public DateTime ExpiresOn {get; set;}
    public string? Guid {get; set;}
  }
}