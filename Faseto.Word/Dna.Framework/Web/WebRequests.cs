using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dna
{
    /// <summary>
    ///
    /// </summary>
    public class WebRequests
    {
        public static async Task<HttpWebResponse> PostAysnc(string url, object content = null, KnownConentSerializers sendType = KnownConentSerializers.Json, KnownConentSerializers returnType = KnownConentSerializers.Json)
        {
            #region Setup
            // Create the web request
            var request = WebRequest.CreateHttp(url);

            // Make it a POST request method
            request.Method = HttpMethod.Post.ToString();

            // Set the appropriate return type
            request.Accept = returnType.ToMimeString();

            // Set the content type
            request.ContentType = sendType.ToMimeString();

            #endregion

            #region Write Content

            // Set the content length
            if (content == null)
            {
                // Set content length to 0
                request.ContentLength = 0;
            }
            // Otherwise...
            else
            {
                // Create content to write
                var contentString = string.Empty;

                // Serialize to Json?
                if (sendType == KnownConentSerializers.Json)
                {
                    // Serialize content to Json string
                    contentString = JsonConvert.SerializeObject(content);
                }
                // Serialize to Xml
                else if(sendType == KnownConentSerializers.Xml)
                {
                    // Create Xml Serializer
                    var xmlSerializer = new XmlSerializer(content.GetType());

                    // Create a string writer to receive the serialized string
                    using (var stringWriter = new StringWriter())
                    {
                        // Serialize the object to a string
                        xmlSerializer.Serialize(stringWriter, content);

                        // Extract the string from the writer
                        contentString = stringWriter.ToString();
                    }
                }
                // Currently unknown
                else
                {
                    //TODO: Throw error once we have Dna framework exception types
                }

                // Get body stream...
                using (var requestStream = await request.GetRequestStreamAsync())
                // Create a stream writer from the body stream...
                using (var streamWriter = new StreamWriter(requestStream))
                    // Write content to HTTP body stream
                    await streamWriter.WriteAsync(contentString);
            }

            #endregion

            // Return the raw server response
            return await request.GetResponseAsync() as HttpWebResponse;
        }

        public static async Task<WebRequestResult<TResponse>> PostAysnc(string url, object content = null, KnownConentSerializers sendType = KnownConentSerializers.Json, KnownConentSerializers returnType = KnownConentSerializers.Json)
        {
            // Make the standard Post call first
            var serverResponse = await PostAysnc(url, content, sendType, returnType);

            // Create a result
            var result = serverResponse.CreateWebRequestResult<TResponse>();

            // If the response status code is not 200...
            if(result.StatusCode != HttpStatusCode.OK)
            {
                // Call failed
                // TODO: Localize string 
                result.ErrorMessage = $"Server returned unsuccessful status code. {serverResponse.StatusCode} {serverResponse.StatusDescription}";

                // Done
                return result;
            }

            // If we have no content to deserialize...
            if (result.RawServerResponse.IsNullOrEmpty())
                // Done
                return result;

            // Deserialize raw response
            try
            {
                // if the server response type was not the excpeted tyep...
                if(!serverResponse.ConentType.ToLower().Contains(returnType.ToMimeString().ToLower()))
                {
                    // Failed due to unexpected return type
                    result.ErrorMessage = $"Server did not return data in expected type. Expected {returnType.ToMimeString()}, received {serverResponse.ContentType}";

                    // Done
                    return result;
                }

                // Json?
                if (returnType== KnownConentSerializers.Json)
                {
                    // Deserialize Json String
                    result.ServerResponse = JsonConvert.DeserializeObject<TResponse>(result.RawServerResponse);
                    
                }
                // Xml?
                else if (returnType == KnownConentSerializers.Xml)
                {
                    // Create Xml Serializer
                    var xmlSerializer = new XmlSerializer(typeof(TResponse));

                    // Create a memory stream for the raw string data
                    using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result.RawServerResponse)))
                        // Deserialize XML string 
                        result.ServerResponse = (TResponse)xmlSerializer.Deserialize(memoryStream);
                } 
                // Unknown 
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
                // If deserialize failed then set error message
                result.ErrorMessage = "Failed to deserialzie server response to the excepted type";

                // Done
                return result;
            }
            
            // Return result
            return result;
        }
