using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Enums;
using Application.Exceptions;
using Application.Extensions;
using Application.Models;
using Newtonsoft.Json;

// https://stackoverflow.com/questions/21097730/usage-of-ensuresuccessstatuscode-and-handling-of-httprequestexception-it-throws
// https://stackoverflow.com/questions/9620278/how-do-i-make-calls-to-a-rest-api-using-c
// https://www.codeguru.co.in/2021/06/generic-httpclient-wrapper-in-c.html
// https://makolyte.com/csharp-how-to-get-the-status-code-when-using-httpclient/
// https://stackoverflow.com/questions/27793761/httpclient-vs-httpwebrequest-for-better-performance-security-and-less-connectio
namespace Infrastructure.Helpers.RestClient
{
    public class RestClientV2 : IRestClientV2
    {
        private HttpClient GetHttpClient(string baseAddress, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, TimeSpan? timeout = null)
        {
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = timeout ?? TimeSpan.FromSeconds(100)
            };

            if (requestHeaders != null)
            {
                foreach (var requestHeader in requestHeaders)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(requestHeader.Key, requestHeader.Value);
                }
            }

            return httpClient;
        }

        public async Task<RestClientResult> Get<T>(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, TimeSpan? timeout = null)
        {
            try
            {
                using (var httpClient = GetHttpClient(baseAddress, requestHeaders, timeout))
                {
                    using (var result = await httpClient.GetAsync(requestUri))
                    {
                        await result.EnsureSuccessStatusCodeDevDefined();
                        return new RestClientResult(ApiStatus.Success, result.StatusCode, await result.GetResultContent<T>());
                    }
                }
            }
            catch (SimpleHttpResponseException exception)
            {
                return new RestClientResult(ApiStatus.WebException, exception.StatusCode, exception.Message);
            }
            catch (Exception ex)
            {
                return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, JsonConvert.SerializeObject(ex));
            }
        }

        public async Task<RestClientResult> Post<T>(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, object contentValue, TimeSpan? timeout = null)
        {
            try
            {
                using (var httpClient = GetHttpClient(baseAddress, requestHeaders, timeout))
                {
                    using (var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json"))
                    {
                        using (var result = await httpClient.PostAsync(requestUri, content))
                        {
                            await result.EnsureSuccessStatusCodeDevDefined();
                            return new RestClientResult(ApiStatus.Success, result.StatusCode, await result.GetResultContent<T>());
                        }
                    }
                }
            }
            catch (SimpleHttpResponseException exception)
            {
                return new RestClientResult(ApiStatus.WebException, exception.StatusCode, exception.Message);
            }
            catch (Exception ex)
            {
                return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, JsonConvert.SerializeObject(ex));
            }
        }

        public async Task<RestClientResult> Post(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, object contentValue, TimeSpan? timeout = null)
        {
            try
            {
                using (var httpClient = GetHttpClient(baseAddress, requestHeaders, timeout))
                {
                    using (var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json"))
                    {
                        using (var result = await httpClient.PostAsync(requestUri, content))
                        {
                            await result.EnsureSuccessStatusCodeDevDefined();
                            return new RestClientResult(ApiStatus.Success, result.StatusCode);
                        }
                    }
                }
            }
            catch (SimpleHttpResponseException exception)
            {
                return new RestClientResult(ApiStatus.WebException, exception.StatusCode, exception.Message);
            }
            catch (Exception ex)
            {
                return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, JsonConvert.SerializeObject(ex));
            }
        }

        public async Task<RestClientResult> Put<T>(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, object contentValue, TimeSpan? timeout = null)
        {
            try
            {
                using (var httpClient = GetHttpClient(baseAddress, requestHeaders, timeout))
                {
                    using (var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json"))
                    {
                        using (var result = await httpClient.PutAsync(requestUri, content))
                        {
                            await result.EnsureSuccessStatusCodeDevDefined();
                            return new RestClientResult(ApiStatus.Success, result.StatusCode, await result.GetResultContent<T>());
                        }
                    }
                }
            }
            catch (SimpleHttpResponseException exception)
            {
                return new RestClientResult(ApiStatus.WebException, exception.StatusCode, exception.Message);
            }
            catch (Exception ex)
            {
                return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, JsonConvert.SerializeObject(ex));
            }
        }

        public async Task<RestClientResult> Put(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, object contentValue, TimeSpan? timeout = null)
        {
            try
            {
                using (var httpClient = GetHttpClient(baseAddress, requestHeaders, timeout))
                {
                    using (var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json"))
                    {
                        using (var result = await httpClient.PutAsync(requestUri, content))
                        {
                            await result.EnsureSuccessStatusCodeDevDefined();
                            return new RestClientResult(ApiStatus.Success, result.StatusCode);
                        }
                    }
                }
            }
            catch (SimpleHttpResponseException exception)
            {
                return new RestClientResult(ApiStatus.WebException, exception.StatusCode, exception.Message);
            }
            catch (Exception ex)
            {
                return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, JsonConvert.SerializeObject(ex));
            }
        }

        public async Task<RestClientResult> Delete(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, TimeSpan? timeout = null)
        {
            try
            {
                using (var httpClient = GetHttpClient(baseAddress, requestHeaders, timeout))
                {
                    using (var result = await httpClient.DeleteAsync(requestUri))
                    {
                        await result.EnsureSuccessStatusCodeDevDefined();
                        return new RestClientResult(ApiStatus.Success, result.StatusCode);
                    }
                }
            }
            catch (SimpleHttpResponseException exception)
            {
                return new RestClientResult(ApiStatus.WebException, exception.StatusCode, exception.Message);
            }
            catch (Exception ex)
            {
                return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, JsonConvert.SerializeObject(ex));
            }
        }
    }
}