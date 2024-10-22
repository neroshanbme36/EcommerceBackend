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
namespace Application.Helpers
{
  public class RestClient : IDisposable
  {
    private readonly HttpClient _httpClient;
    private bool disposed = false;

    public RestClient(string baseAddress, TimeSpan? timeout = null)
    {
      _httpClient = GetHttpClient(baseAddress, timeout);
    }

    public RestClient(string baseAddress, IReadOnlyList<KeyValuePair<string, string>> requestHeaders, TimeSpan? timeout = null)
    {
      _httpClient = GetHttpClient(baseAddress, timeout);
      foreach (var requestHeader in requestHeaders)
      {
        _httpClient.DefaultRequestHeaders.Add(requestHeader.Key, requestHeader.Value);
      }
    }

    private HttpClient GetHttpClient(string baseAddress, TimeSpan? timeout = null)
    {
      return new HttpClient()
      {
        BaseAddress = new Uri(baseAddress),
        Timeout = timeout ?? TimeSpan.FromSeconds(100)
      };
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
      if (!disposed && disposing)
      {
        if (_httpClient != null)
        {
          _httpClient.Dispose();
        }
        disposed = true;
      }
    }

    public async Task<RestClientResult> Get<T>(string requestUri)
    {
      try
      {
        using (var result = await _httpClient.GetAsync(requestUri))
        {
          await result.EnsureSuccessStatusCodeDevDefined();
          return new RestClientResult(ApiStatus.Success, result.StatusCode, await result.GetResultContent<T>());
        }
      }
      catch (SimpleHttpResponseException exception)
      {
        return new RestClientResult(ApiStatus.WebException, exception.StatusCode, exception.Message);
      }
      catch (Exception ex)
      {
        return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, ex.GetExceptionJson());
      }
    }

    public async Task<RestClientResult> Post<T>(string requestUri, object contentValue)
    {
      try
      {
        using (var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json"))
        {
          using (var result = await _httpClient.PostAsync(requestUri, content))
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
        return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, ex.GetExceptionJson());
      }
    }

    public async Task<RestClientResult> Post(string requestUri, object contentValue)
    {
      try
      {
        using (var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json"))
        {
          using (var result = await _httpClient.PostAsync(requestUri, content))
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
        return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, ex.GetExceptionJson());
      }
    }

    public async Task<RestClientResult> Put<T>(string requestUri, object contentValue)
    {
      try
      {
        using (var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json"))
        {
          using (var result = await _httpClient.PutAsync(requestUri, content))
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
        return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, ex.GetExceptionJson());
      }
    }

    public async Task<RestClientResult> Put(string requestUri, object contentValue)
    {
      try
      {
        using (var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json"))
        {
          using (var result = await _httpClient.PutAsync(requestUri, content))
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
        return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, ex.GetExceptionJson());
      }
    }

    public async Task<RestClientResult> Delete(string requestUri)
    {
      try
      {
        using (var result = await _httpClient.DeleteAsync(requestUri))
        {
          await result.EnsureSuccessStatusCodeDevDefined();
          return new RestClientResult(ApiStatus.Success, result.StatusCode);
        }
      }
      catch (SimpleHttpResponseException exception)
      {
        return new RestClientResult(ApiStatus.WebException, exception.StatusCode, exception.Message);
      }
      catch (Exception ex)
      {
        return new RestClientResult(ApiStatus.Exception, HttpStatusCode.GatewayTimeout, ex.GetExceptionJson());
      }
    }
  }
}