// Added for common http requests : surenderssm

namespace WordFly.ServiceClientMe
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Basic Http Common Functions
    /// </summary>
    public static class HttpUtility
    {
        // TODO: think over the perf
        private static HttpClient client;

        static HttpUtility()
        {
            client = new HttpClient();
        }

        /// <summary>
        /// Make the HTTPOST on the Given the URI with given JSON data
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="jsonDataToPost"></param>
        /// <returns></returns>
        public static async Task<string> PostRequest(string requestUri, string jsonDataToPost)
        {
            string result = null;
            HttpContent content = new System.Net.Http.StringContent(jsonDataToPost);
            content.Headers.Clear();
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            using (HttpResponseMessage response = await client.PostAsync(requestUri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // TODO: User defined exceptions
                    throw new Exception(String.Format("Request Failed : {0}", response.ToString()));
                }
            }
            return result;
        }
    }
}




