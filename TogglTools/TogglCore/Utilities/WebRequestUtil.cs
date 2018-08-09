using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using CoreySutton.Utilities;

namespace CoreySutton.TogglTools.TogglCore
{
    public static class WebUtil
    {
        public static void SetBasicAuthHeader(
            WebRequest request,
            string username,
            string password)
        {
            ArgUtil.NotNull(request, nameof(request));
            ArgUtil.NotNull(username, nameof(username));
            ArgUtil.NotNull(password, nameof(password));

            var authInfo = username + ":" + password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers.Add(HttpRequestHeader.Authorization, $"Basic {authInfo}");
        }

        public static string GetResponseString(WebResponse response)
        {
            ArgUtil.NotNull(response, nameof(response));

            var dataStream = response.GetResponseStream();
            if (dataStream == null)
            {
                throw new NullReferenceException("Server returned an empty data stream");
            }

            using (var reader = new StreamReader(dataStream))
            {
                var responseFromServer = reader.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(responseFromServer))
                {
                    return responseFromServer;
                }

                throw new NullReferenceException("Reader returned an empty response string from data stream");
            }
        }

        public static IEnumerable<string> GetHeaders(string name, HttpRequestHeaders requestHeaders)
        {
            ArgUtil.NotNull(name, nameof(name));
            ArgUtil.NotNull(requestHeaders, nameof(requestHeaders));

            bool gotHeader = requestHeaders.TryGetValues(
                name,
                out IEnumerable<string> headers);

            if (gotHeader == false)
            {
                throw new Exception($"Failed to retrieve {name} headers");
            }

            return headers;
        }

        public static string GetHeader(string name, HttpRequestHeaders requestHeaders)
        {
            ArgUtil.NotNull(name, nameof(name));
            ArgUtil.NotNull(requestHeaders, nameof(requestHeaders));

            List<string> headers = GetHeaders(name, requestHeaders).ToList<string>();

            if (headers.Count == 0)
            {
                throw new Exception($"Failed to retrieve {name} header");
            }

            if (headers.Count > 1)
            {
                throw new Exception($"Retrieved {headers.Count} {name} headers, expected 1");
            }

            return headers[0];
        }
    }
}
