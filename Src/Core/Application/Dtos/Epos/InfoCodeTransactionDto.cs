using System;

namespace Application.Dtos.CloudStoreEpos.Epos
{
    public class InfoCodeTransactionDto
    {
        public long HeaderId {get; set;}
        public string KeyId {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public decimal Quantity {get; set;}
        public decimal Price {get; set;}
        public decimal Amount {get; set;}
        public string DepartmentId {get; set;} = string.Empty;
        public string? ProductGroupId {get; set;}
        public string? ProductSubGroupId {get; set;}
        public string Prompt {get; set;} = string.Empty;
        public DateTime MinDate {get; set;}
        public int Priority {get; set;}
        public bool Authorized {get; set;}
        public string? RefusalReason {get; set;}
    }
}