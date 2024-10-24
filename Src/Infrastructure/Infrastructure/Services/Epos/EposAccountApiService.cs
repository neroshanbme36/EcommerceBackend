using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts.Infrastructure.Epos;
using Application.Dtos.CloudStoreEpos.Epos.Account;
using Application.Enums;
using Application.Exceptions;
using Application.Models;
using Infrastructure.Helpers.RestClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services.Epos
{
    public class EposAccountApiService : IEposAccountApiService
    {
        private readonly string ServerUrl;
        private readonly string ApiVersion;
        private readonly IRestClientV2 _restClient;

        public EposAccountApiService(IRestClientV2 restClient, IOptions<MicroservicesBaseUrl> microservicesBaseUrl)
        {
            _restClient = restClient;
            ServerUrl = microservicesBaseUrl.Value.EposServerUrl;
            ApiVersion = $"/api/v{microservicesBaseUrl.Value.EposApiVersion}";
        }

        public async Task<string> GetAccessToken(string eposApiKey, string deviceId)
        {
            EposLoginDto request = new EposLoginDto
            {
                Username = "101",
                Password = "101"
            };
            EposUserDto? userDto = await Login(eposApiKey, deviceId, request);
            return userDto != null ? userDto.Token : string.Empty;
        }

        private async Task<EposUserDto?> Login(string eposApiKey, string deviceId, EposLoginDto request)
        {
            string requestUrl = $"{ApiVersion}/account/login";

            List<KeyValuePair<string, string>> requestHeaders = new List<KeyValuePair<string, string>> {
                //GetAuthorizationHeader(accessToken),
                GetEposApiKeyHeader(eposApiKey),
                GetDeviceIdHeader(deviceId)
            };

            RestClientResult rcResult = await _restClient.Post<EposUserDto>(ServerUrl, requestUrl, requestHeaders, request);
            if (rcResult.ApiStatus != ApiStatus.Success)
            {
                throw new InternalServerErrorException(JsonConvert.SerializeObject(rcResult.Data));
            }
            if (rcResult.Data == null) return null;
            return (EposUserDto)rcResult.Data;
        }

        // private KeyValuePair<string, string> GetAuthorizationHeader(string accessToken)
        // {
        //     return new KeyValuePair<string, string>(RequestHeaderCodes.AUTHORIZATION, accessToken);
        // }

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