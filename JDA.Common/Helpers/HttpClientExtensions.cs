using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Common.Helpers
{

    /// <summary>
    /// JDA.Common.Helpers.HttpClientExtensions class for HTTP client extensions
    /// </summary>
    public static class HttpClientExtensions
    {
        public static string SecurityToken = "WbLqXuvtLBZT2iBAkGbjLLrtT0ZFw7RoHd14OJty-YZsn077URn2400zQAUA9lZlpT_81PTvO_XYDbGWxVKhbQ";

        /// <summary>
        /// Extension method to Posts json object asynchronous.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient httpClient, string uri, object data)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("RequestToken", "B7A6A1F06D8A48BE826BBD184D0BBE17F224C661DABBD9B581ADAA7F2D56A375");
            httpClient.DefaultRequestHeaders.Add("SecurityToken", SecurityToken);
            var strData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(uri, strData);
        }

        /// <summary>
        /// Extension method to Put json object asynchronous.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient httpClient, string uri, object data)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("RequestToken", "B7A6A1F06D8A48BE826BBD184D0BBE17F224C661DABBD9B581ADAA7F2D56A375");
            httpClient.DefaultRequestHeaders.Add("SecurityToken", SecurityToken);
            var strData = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            
            return await httpClient.PutAsync(uri, strData);
        }

        /// <summary>
        /// Extension method to Get json object asynchronous.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> GetAsJsonAsync(this HttpClient httpClient, string uri)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("SecurityToken", SecurityToken);
                httpClient.DefaultRequestHeaders.Add("RequestToken", "B7A6A1F06D8A48BE826BBD184D0BBE17F224C661DABBD9B581ADAA7F2D56A375");

                return await httpClient.GetAsync(uri);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
    }
}

