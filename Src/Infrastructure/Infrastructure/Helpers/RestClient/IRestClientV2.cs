using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;

namespace Infrastructure.Helpers.RestClient
{
    public interface IRestClientV2
    {
        Task<RestClientResult> Get<T>(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, TimeSpan? timeout = null);
        Task<RestClientResult> Post<T>(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, object contentValue, TimeSpan? timeout = null);
        Task<RestClientResult> Post(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, object contentValue, TimeSpan? timeout = null);
        Task<RestClientResult> Put<T>(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, object contentValue, TimeSpan? timeout = null);
        Task<RestClientResult> Put(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, object contentValue, TimeSpan? timeout = null);
        Task<RestClientResult> Delete(string baseAddress, string requestUri, IReadOnlyList<KeyValuePair<string, string>>? requestHeaders, TimeSpan? timeout = null);
    }
}