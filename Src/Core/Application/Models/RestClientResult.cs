using System.Net;
using Application.Enums;

namespace Application.Models
{
  public class RestClientResult
  {
    public HttpStatusCode StatusCode { get; private set; }
    public object? Data { get; private set; }
    public ApiStatus ApiStatus { get; private set; }

    public RestClientResult(ApiStatus apiStatus, HttpStatusCode statusCode)
    {
      ApiStatus = apiStatus;
      StatusCode = statusCode;
    }

    public RestClientResult(ApiStatus apiStatus, HttpStatusCode statusCode, object? data)
    {
      ApiStatus = apiStatus;
      StatusCode = statusCode;
      Data = data;
    }
  }
}