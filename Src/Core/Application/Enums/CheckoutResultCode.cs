
namespace Application.Enums
{
    public enum CheckoutResultCode
    {
        Error = 0,
        Pending = 1,
        Completed = 2,
        CartNotFound = 90001,
        CartExpired = 90002
    }
}