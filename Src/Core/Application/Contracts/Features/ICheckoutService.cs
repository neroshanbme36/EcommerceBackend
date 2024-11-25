using Application.Dtos.checkout;

namespace Application.Contracts.Features
{
    public interface ICheckoutService
    {
        Task<CheckoutResponseDto> ProcessCheckout(string userId, CheckoutRequestDto request);
    }
}