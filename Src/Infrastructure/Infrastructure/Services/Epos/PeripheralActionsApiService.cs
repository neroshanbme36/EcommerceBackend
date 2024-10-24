using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts.Infrastructure.Epos;
using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Infrastructure.Helpers.RestClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services.Epos
{
    public class PeripheralActionsApiService : IPeripheralActionsApiService
    {
        private readonly string ServerUrl;
        private readonly string ApiVersion;
        private readonly IRestClientV2 _restClient;
        private readonly IEposAccountApiService _eposAccountApiService;

        public PeripheralActionsApiService(IRestClientV2 restClient, IOptions<MicroservicesBaseUrl> microservicesBaseUrl, IEposAccountApiService eposAccountApiService)
        {
            _restClient = restClient;
            ServerUrl = microservicesBaseUrl.Value.EposServerUrl;
            ApiVersion = $"/api/v{microservicesBaseUrl.Value.EposApiVersion}";
            _eposAccountApiService = eposAccountApiService;
        }

        public async Task PrintBookingReceipt(string eposApiKey, string deviceId, string repairId)
        {
            string requestUrl = $"{ApiVersion}/peripheralActions/printer/booking-receipt/{repairId}";
            string accessToken = $"Bearer {await _eposAccountApiService.GetAccessToken(eposApiKey, deviceId)}";

            List<KeyValuePair<string, string>> requestHeaders = new List<KeyValuePair<string, string>> {
                GetAuthorizationHeader(accessToken),
                GetEposApiKeyHeader(eposApiKey),
                GetDeviceIdHeader(deviceId)
            };

            RestClientResult rcResult = await _restClient.Post(ServerUrl, requestUrl, requestHeaders, null);
            if (rcResult.ApiStatus != ApiStatus.Success)
            {
                throw new InternalServerErrorException(JsonConvert.SerializeObject(rcResult.Data));
            }
        }

        public async Task PrintServiceReceipt(string eposApiKey, string deviceId, string repairId)
        {
            string requestUrl = $"{ApiVersion}/peripheralActions/printer/service-receipt/{repairId}";
            string accessToken = $"Bearer {await _eposAccountApiService.GetAccessToken(eposApiKey, deviceId)}";

            List<KeyValuePair<string, string>> requestHeaders = new List<KeyValuePair<string, string>> {
                GetAuthorizationHeader(accessToken),
                GetEposApiKeyHeader(eposApiKey),
                GetDeviceIdHeader(deviceId)
            };

            RestClientResult rcResult = await _restClient.Post(ServerUrl, requestUrl, requestHeaders, null);
            if (rcResult.ApiStatus != ApiStatus.Success)
            {
                throw new InternalServerErrorException(JsonConvert.SerializeObject(rcResult.Data));
            }
        }

        private KeyValuePair<string, string> GetAuthorizationHeader(string accessToken)
        {
            return new KeyValuePair<string, string>(RequestHeaderCodes.AUTHORIZATION, accessToken);
        }

        private KeyValuePair<string, string> GetDeviceIdHeader(string deviceId)
        {
            return new KeyValuePair<string, string>(RequestHeaderCodes.DEVICE_ID, deviceId);
        }

        private KeyValuePair<string, string> GetEposApiKeyHeader(string eposApiKey)
        {
            return new KeyValuePair<string, string>(RequestHeaderCodes.EPOS_API_KEY, eposApiKey);
        }
    }
}