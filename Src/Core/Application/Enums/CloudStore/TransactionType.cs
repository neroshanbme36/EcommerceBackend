namespace Domain.Enums.CloudStoreEpos
{
    public enum TransactionType
    {
        Sales = 0,
        Refund = 1,
        TenderDeclare = 2,
        TenderRemove = 3,
        TenderFloat = 4,
        Drawer = 7,
        Repair = 99999,
    }
}