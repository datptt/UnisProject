using System;
using Microsoft.AspNetCore.Http;

namespace Unis.API
{

    internal static class RequestHeaderExtensions
    {
        internal static string GetBearerToken(this IHeaderDictionary dic)
        {
            if (dic?.ContainsKey("Authorization") != true) return null;

            string requestHeader = dic["Authorization"];
            return string.IsNullOrEmpty(requestHeader) ? null : requestHeader.Replace("Bearer ", string.Empty);
        }
    }

}
