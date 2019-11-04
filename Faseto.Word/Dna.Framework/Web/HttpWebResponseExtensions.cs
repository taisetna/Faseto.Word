using System.IO;
using System.Net;

namespace Dna
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpWebResponseExtensions
    {
        public static WebRequestResult<TResponse> CreateWebRequestResult<TResponse>(this HttpWebResponse serverResponse)
        {
            // return a new web request result
            var result =  new WebRequestResult<TResponse>
            {
                // Content Type
                ContentType = serverResponse.ContentType,

                // Headers
                Headers = serverResponse.Headers,

                // Cookies
                Cookies = serverResponse.Cookies,

                // Status Code
                StatusCode = serverResponse.StatusCode,

                // Status Description
                StatusDescription = serverResponse.StatusDescription,
            };

            // If we got a successful response...
            if(result.StatusCode == HttpStatusCode.OK)
            {
                // Open the response stream...
                using (var responseStream = serverResponse.GetResponseStream())
                // Get a stream reader
                using (var streamReader = new StreamReader(responseStream))
                    // Read in the response body...
                    result.RawServerResponse = streamReader.ReadToEnd();
            }
        }
    }
}
