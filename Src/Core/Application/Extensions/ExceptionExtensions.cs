using System;
using Application.Models;
using Newtonsoft.Json;

namespace Application.Extensions
{
  public static class ExceptionExtensions
  {
    public static string GetExceptionJson(this Exception ex)
    {
      ExceptionModel exc = new ExceptionModel
      {
        Source = ex.Source,
        Message = ex.Message,
        TargetSite = ex.TargetSite?.ToString(),
        StackTrace = ex.StackTrace
      };
      return JsonConvert.SerializeObject(exc);
    }
  }
}