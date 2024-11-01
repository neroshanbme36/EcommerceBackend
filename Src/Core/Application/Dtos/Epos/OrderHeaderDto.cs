using System;
using Domain.Enums.CloudStoreEpos;

namespace Application.Dtos.CloudStoreEpos.Epos
{
    public class OrderHeaderDto
    {
        public long Id { get; set; }
        public TransactionType TransType { get; set; }
        public OrderType OrderType { get; set; }
        public string UserId { get; set; }
        public long TakeawayId { get; set; }
        public long DeliveryId { get; set; }
        public bool IsScheduledOrder { get; set; }
        public DateTime RequestedOn { get; set; }
        public string LocationId { get; set; }
        public string LocationDesc { get; set; }
        public int TableId { get; set; }
        public int Seats { get; set; }
        public string FreeText { get; set; }
        public string CustomerId { get; set; }
        public string LoyaltyCardNo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime CreatedOnGmt { get; set; }
        public string StaffName { get; set; }
        public decimal SalesAmount { get; set; }
        public string DeviceId {get; set;}
        public string ZReportId {get; set;}
        public string RepairId {get; set;}
        public string? Guid {get; set;}
    }
}