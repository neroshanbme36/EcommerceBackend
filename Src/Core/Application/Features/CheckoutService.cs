using Application.Contracts.Features;
using Application.Contracts.Infrastructure.Epos;
using Application.Contracts.Persistence;
using Application.Dtos.checkout;
using Application.Dtos.CloudStoreEpos.Epos.Payment;
using Application.Enums;
using Application.Enums.CloudStoreEpos;
using Application.Exceptions;
using Application.Models;
using Domain.Crm.Entities;
using Domain.Entities;
using Domain.Enums.CloudStoreEpos;
using Microsoft.Extensions.Options;

namespace Application.Features
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDeviceService _deviceService;
        private readonly IAppAccessTokenService _appAccessTokenService;
        private readonly IEposAccountApiService _eposAccountApiService;
        private readonly string _eposAppName;
        private readonly IEposPaymentApiService _eposPaymentApiService;

        public CheckoutService(IUnitOfWork uow, IDeviceService deviceService, IAppAccessTokenService appAccessTokenService,
        IEposAccountApiService eposAccountApiService, IOptions<MicroservicesBaseUrl> microservicesBaseUrl, IEposPaymentApiService eposPaymentApiService)
        {
            _uow = uow;
            _deviceService = deviceService;
            _appAccessTokenService = appAccessTokenService;
            _eposAccountApiService = eposAccountApiService;
            _eposPaymentApiService = eposPaymentApiService;
            _eposAppName = microservicesBaseUrl.Value.EposAppName;
        }

        public async Task<CheckoutResponseDto> ProcessCheckout(string userId, CheckoutRequestDto request)
        {
            EposTransactionHeader? eposTransactionHeader = await _uow.EposTransactionHeaderRepository.GetEposTransactionHeaderByGuid(request.CartId);
            if (eposTransactionHeader == null) return new CheckoutResponseDto { Code = CheckoutResultCode.CartNotFound, Message = "Cart doesnt exist" };
            if (eposTransactionHeader.ExpiresOn <= DateTime.Now) return new CheckoutResponseDto { Code = CheckoutResultCode.CartExpired, Message = "Cart expired" };
            if (eposTransactionHeader.SalesAmount <= 0.00m) return new CheckoutResponseDto("Sales amount is invalid" );
            // opening hours
            #region SHIPPING RANGE
            ShippingZone? shippingZone = null;
            if (eposTransactionHeader.OrderType == OrderType.Delivery)
            {
                shippingZone = await _uow.ShippingZoneRepository.GetShippingZone(request.ShippingAddress.CountryId, request.ShippingAddress.Postcode);
                if (shippingZone == null) return new CheckoutResponseDto("Out of shipping range" );
            }
            #endregion SHIPPING RANGE
            // Validate Location And Table
            #region PRODUCT AVAILABILITY
            string productAvailabiltyMessage = await GetProductAvailabiltyMessage(eposTransactionHeader.EposTransactionLines);
            if (!string.IsNullOrWhiteSpace(productAvailabiltyMessage)) return new CheckoutResponseDto(productAvailabiltyMessage);
            #endregion PRODUCT AVAILABILITY
            // preordermax no of days
            // order limits
            // Validate Loyalty

            eposTransactionHeader = MapToEposTransactionHeader(userId, eposTransactionHeader, request);
            await _uow.EposTransactionHeaderRepository.Update(eposTransactionHeader);
            bool isSaved = await SaveUnCommitedChanges();
            if (!isSaved) return new CheckoutResponseDto("Cart save failed");

            CheckoutResponseDto checkoutResponseDto = new CheckoutResponseDto("Payment is invalid");

            if (request.PaymentMethod == PaymentMethod.OnlineCardPayment)
                checkoutResponseDto = await PostToEposPayment(request, eposTransactionHeader.SalesAmount);

            await SaveUnCommitedChanges(); // dont remove need to mark if token is expired
            return checkoutResponseDto;
        }

        private async Task<string> GetProductAvailabiltyMessage(IReadOnlyList<EposTransactionLine> eposTransactionLines)
        {
            IReadOnlyList<EposTransactionLine> unvoidedProductEposTransLines = eposTransactionLines
                .Where(c => c.EntryType == EntryType.Product && !c.LineStatus)
                .ToList();

            IReadOnlyList<string> cartProductItemNos = unvoidedProductEposTransLines
                .Select(c => c.KeyId)
                .ToList();

            IReadOnlyList<Product> manageStockProducts = await _uow.ProductRepository.GetProductsWithManageStockByItemNos(cartProductItemNos);

            List<string> productUnavailabiltyMsgs = new List<string>();
            foreach (Product product in manageStockProducts)
            {
                decimal cartQty = unvoidedProductEposTransLines.Where(c => c.KeyId == product.ItemNo).Sum(c => c.Quantity);
                if (product.StockQuantity <= 0.00m)
                {
                    productUnavailabiltyMsgs.Add($"{product.Description}(No stock)");
                }
                else if (cartQty > product.StockQuantity)
                {
                    decimal unavailableQty = product.StockQuantity - cartQty;
                    productUnavailabiltyMsgs.Add($"{product.Description}({unavailableQty} Qty not in stock)");
                }
            }

            if (productUnavailabiltyMsgs.Count > 0) return string.Join(",", productUnavailabiltyMsgs);
            return string.Empty;
        }

        private EposTransactionHeader MapToEposTransactionHeader(string userId, EposTransactionHeader eposTransactionHeader, CheckoutRequestDto request)
        {
            eposTransactionHeader.OrderType = request.OrderType;
            eposTransactionHeader.IsScheduledOrder = request.IsScheduledOrder;
            eposTransactionHeader.RequestedOn = request.RequestedOn;
            eposTransactionHeader.FreeText = request.FreeText;

            eposTransactionHeader.EcommCustomerId = userId;
            eposTransactionHeader.CustEmail = request.Email;
            if (request.BillingAddress != null)
            {
                eposTransactionHeader.CustFirstName = request.BillingAddress.FirstName;
                eposTransactionHeader.CustLastName = request.BillingAddress.LastName;
                eposTransactionHeader.CustPhone = request.BillingAddress.Phone;

                eposTransactionHeader.BillCustAddressId = request.BillingAddress.Id;
                eposTransactionHeader.BillAddressLine1 = request.BillingAddress.AddressLine1;
                eposTransactionHeader.BillAddressLine2 = request.BillingAddress.AddressLine2;
                eposTransactionHeader.BillAddressLine3 = request.BillingAddress.AddressLine3;
                eposTransactionHeader.BillAddressLine4 = request.BillingAddress.AddressLine4;
                eposTransactionHeader.BillCity = request.BillingAddress.City;
                eposTransactionHeader.BillState = request.BillingAddress.State;
                eposTransactionHeader.BillPostcode = request.BillingAddress.Postcode;
                eposTransactionHeader.BillCountryId = request.BillingAddress.CountryId;
            }
            if (request.ShippingAddress != null)
            {
                eposTransactionHeader.CustFirstName = request.ShippingAddress.FirstName;
                eposTransactionHeader.CustLastName = request.ShippingAddress.LastName;
                eposTransactionHeader.CustPhone = request.ShippingAddress.Phone;

                eposTransactionHeader.DeliCustAddressId = request.ShippingAddress.Id;
                eposTransactionHeader.DeliAddressLine1 = request.ShippingAddress.AddressLine1;
                eposTransactionHeader.DeliAddressLine2 = request.ShippingAddress.AddressLine2;
                eposTransactionHeader.DeliAddressLine3 = request.ShippingAddress.AddressLine3;
                eposTransactionHeader.DeliAddressLine4 = request.ShippingAddress.AddressLine4;
                eposTransactionHeader.DeliCity = request.ShippingAddress.City;
                eposTransactionHeader.DeliState = request.ShippingAddress.State;
                eposTransactionHeader.DeliPostcode = request.ShippingAddress.Postcode;
                eposTransactionHeader.DeliCountryId = request.ShippingAddress.CountryId;
            }
            return eposTransactionHeader;
        }

        private async Task<CheckoutResponseDto> PostToEposPayment(CheckoutRequestDto request, decimal amount)
        {
            CheckoutResponseDto checkoutResponseDto = new CheckoutResponseDto();
            Device device = await _deviceService.GetEcommerceDevice();
            PaymentDto eposPaymentRequest = new PaymentDto
            {
                Guid = request.CartId,
                TenderType = TenderType.Card,
                Amount = amount,
            };
            AppAccessToken appAccessToken = await _appAccessTokenService.GetAppAccessToken(_eposAppName, _eposAccountApiService.GetAccessToken(device.ProductKey, device.Id));
            try
            {
                PaymentResultDto? eposPaymentResult = await _eposPaymentApiService.PostPayment(appAccessToken.Token, device.ProductKey, device.Id, eposPaymentRequest);
                if (eposPaymentResult != null)
                {
                    checkoutResponseDto.Code = (CheckoutResultCode)(int)eposPaymentResult.Code;
                    checkoutResponseDto.Message = eposPaymentResult.Message;
                    checkoutResponseDto.Order = eposPaymentResult.Order;
                }
            }
            catch (UnauthorizedException ex)
            {
                await _appAccessTokenService.MakeTokenExpire(appAccessToken);
                //await SaveUnCommitedChanges();
                checkoutResponseDto.Code = CheckoutResultCode.Error;
                checkoutResponseDto.Message = ex.Message;
            }
            catch (InternalServerErrorException ex)
            {
                //await SaveUnCommitedChanges();
                checkoutResponseDto.Code = CheckoutResultCode.Error;
                checkoutResponseDto.Message = ex.Message;
            }
            //await SaveUnCommitedChanges();
            return checkoutResponseDto;
        }

        private async Task<bool> SaveUnCommitedChanges()
        {
            bool result = true;
            if (_uow.HasChanges())
                result = await _uow.Save() > 0;
            return result;
        }
    }
}