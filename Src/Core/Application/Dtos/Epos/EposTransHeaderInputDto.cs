using System;
using Domain.Enums.CloudStoreEpos;

namespace Application.Dtos.CloudStoreEpos.Epos
{
    public class EposTransHeaderInputDto
    {
        public long Id {get; set;}
        public TransactionType TransType {get; set;}
        public OrderType OrderType {get; set;}
        public bool IsScheduledOrder {get; set;}
        public DateTime RequestedOn {get; set;}
        public string? LocationId {get; set;}
        public int TableId {get; set;}
        public int Seats {get; set;}
        public string? FreeText {get; set;} 
        public string? CustomerId {get; set;} 
        public string? LoyaltyCardNo {get; set;} 
        public string? RepairId {get; set;}
    }
}