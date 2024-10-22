using System;
using System.Net;

namespace Application.Exceptions
{
  public class SimpleHttpResponseException : Exception
  {
    public HttpStatusCode StatusCode { get; private set; }
    public string Content { get; private set; }

    public SimpleHttpResponseException(HttpStatusCode statusCode, string content) : base(content)
    {
      StatusCode = statusCode;
      Content = content;
    }
  }
}