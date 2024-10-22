using System.Net.Http;
using System.Threading.Tasks;
using Application.Exceptions;
using Newtonsoft.Json;

namespace Application.Extensions
{
  public static class HttpResponseMessageExtensions
  {
    public static async Task EnsureSuccessStatusCodeDevDefined(this HttpResponseMessage response)
    {
      if (response.IsSuccessStatusCode) return;
      throw new SimpleHttpResponseException(response.StatusCode, await response.GetResultContentString());
    }

    private static async Task<string> GetResultContentString(this HttpResponseMessage response)
    {
      string content = await response.Content.ReadAsStringAsync();
      if (response.Content != null)
        response.Content.Dispose();

      return content;
    }

    public static async Task<T?> GetResultContent<T>(this HttpResponseMessage response)
    {
      return JsonConvert.DeserializeObject<T>(await response.GetResultContentString());
    }
  }
}