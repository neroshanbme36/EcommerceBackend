using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts.Infrastructure.Epos;
using Application.Dtos.CloudStoreEpos.Epos;
using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Infrastructure.Helpers.RestClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services.Epos
{
    public class EposTransactionApiService : IEposTransactionApiService
    {
        private readonly string ServerUrl;
        private readonly string ApiVersion;
        private readonly IRestClientV2 _restClient;

        public EposTransactionApiService(IRestClientV2 restClient, IOptions<MicroservicesBaseUrl> microservicesBaseUrl)
        {
            _restClient = restClient;
            ServerUrl = microservicesBaseUrl.Value.EposServerUrl;
            ApiVersion = $"/api/v{microservicesBaseUrl.Value.EposApiVersion}";
        }

        public async Task<OrderDto?> GetTransactionByGuid(string token, string eposApiKey, string deviceId, string guid)
        {
            string requestUrl = $"{ApiVersion}/eposTransactions/search-by-guid/{guid}";
            string accessToken = $"Bearer {token}";

            List<KeyValuePair<string, string>> requestHeaders = new List<KeyValuePair<string, string>> {
                GetAuthorizationHeader(accessToken),
                GetEposApiKeyHeader(eposApiKey),
                GetDeviceIdHeader(deviceId)
            };

            RestClientResult rcResult = await _restClient.Get<OrderDto>(ServerUrl, requestUrl, requestHeaders);
            if (rcResult.ApiStatus != ApiStatus.Success) 
            {
                string jsonErrorMsg = JsonConvert.SerializeObject(rcResult.Data);
                if (rcResult.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedException(jsonErrorMsg);
                else if (rcResult.StatusCode == HttpStatusCode.NotFound) 
                    throw new NotFoundException(jsonErrorMsg, guid);
                else
                    throw new InternalServerErrorException(jsonErrorMsg);
            }
            if (rcResult.Data == null) return null;
            return (OrderDto)rcResult.Data;
        }


        public async Task<OrderDto?> InsertOrUpdateHeader(string token, string eposApiKey, string deviceId, EposTransHeaderInputDto request)
        {
            string requestUrl = $"{ApiVersion}/eposTransactions/insertOrUpdateHeader";
            string accessToken = $"Bearer {token}";

            List<KeyValuePair<string, string>> requestHeaders = new List<KeyValuePair<string, string>> {
                GetAuthorizationHeader(accessToken),
                GetEposApiKeyHeader(eposApiKey),
                GetDeviceIdHeader(deviceId)
            };

            RestClientResult rcResult = await _restClient.Post<OrderDto>(ServerUrl, requestUrl, requestHeaders, request);
            if (rcResult.ApiStatus != ApiStatus.Success) 
            {
                string jsonErrorMsg = JsonConvert.SerializeObject(rcResult.Data);
                if (rcResult.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedException(jsonErrorMsg);
                throw new InternalServerErrorException(jsonErrorMsg);
            }
            if (rcResult.Data == null) return null;
            return (OrderDto)rcResult.Data;
        }

        public async Task<ProductSearchResultDto?> InsertOrUpdateLine(string token, string eposApiKey, string deviceId, EposTransLineInputDto request)
        {
            string requestUrl = $"{ApiVersion}/eposTransactions/insertOrUpdateLine";
            string accessToken = $"Bearer {token}";

            List<KeyValuePair<string, string>> requestHeaders = new List<KeyValuePair<string, string>> {
                GetAuthorizationHeader(accessToken),
                GetEposApiKeyHeader(eposApiKey),
                GetDeviceIdHeader(deviceId)
            };

            RestClientResult rcResult = await _restClient.Post<ProductSearchResultDto>(ServerUrl, requestUrl, requestHeaders, request);
            if (rcResult.ApiStatus != ApiStatus.Success) 
            {
                string jsonErrorMsg = JsonConvert.SerializeObject(rcResult.Data);
                if (rcResult.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedException(jsonErrorMsg);
                throw new InternalServerErrorException(jsonErrorMsg);
            }
            if (rcResult.Data == null) return null;
            return (ProductSearchResultDto)rcResult.Data;
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