using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.Helpers.RestClient
{
    public static class DictionaryExtensions
    {
        public static string ToQueryString(this Dictionary<string, string> source)
        {
            return string.Join("&", source.Select(kvp => string.Format("{0}={1}", HttpUtility.UrlEncode(kvp.Key), HttpUtility.UrlEncode(kvp.Value))).ToArray());
        }
    }
}