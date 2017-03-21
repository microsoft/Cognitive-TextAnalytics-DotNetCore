using Microsoft.ProjectOxford.Text.Core;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ProjectOxford.Text.Topic
{
    /// <summary>
    /// Client for interacting with the Text Analytics topic detection API.
    /// </summary>
    /// <seealso cref="Microsoft.ProjectOxford.Text.Core.TextClient" />
    public class TopicClient : TextClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TopicClient"/> class.
        /// </summary>
        /// <param name="apiKey">The Text Analytics API key.</param>
        public TopicClient(string apiKey) : base(apiKey)
        {
            this.Url = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/topics";
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Starts processing a collection for topics.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The URL to check processing status.</returns>
        public string StartTopicProcessing(TopicRequest request)
        {
            return StartTopicProcessingAsync(request).Result;
        }

        /// <summary>
        /// Starts processing a collection for topics asynchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The URL to check processing status.</returns>
        public async Task<string> StartTopicProcessingAsync(TopicRequest request)
        {
            var reqJson = JsonConvert.SerializeObject(request);
            byte[] reqData = Encoding.UTF8.GetBytes(reqJson);

            if (request.MinDocumentsPerWord > 0)
            {
                this.Url = string.Format("{0}?minDocumentsPerWord={1}", this.Url, request.MinDocumentsPerWord);

                if (request.MaxDocumentsPerWord > 0)
                {
                    this.Url = string.Format("{0}&maxDocumentsPerWord={1}", this.Url, request.MaxDocumentsPerWord);
                }
            }
            else if (request.MaxDocumentsPerWord > 0)
            {
                this.Url = string.Format("{0}?maxDocumentsPerWord={1}", this.Url, request.MaxDocumentsPerWord);
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(this.Url);
            req.Headers["Ocp-Apim-Subscription-Key"] = this.ApiKey;
            req.ContentType = "application/json";
            req.Accept = "application/json";
            //req.ContentLength = reqData.Length;
            req.Method = "POST";

            using (var reqStream = await req.GetRequestStreamAsync())
            {
                reqStream.Write(reqData, 0, reqData.Length);
            }

            var response = await req.GetResponseAsync();

            var operationUrl = response.Headers["Operation-Location"];

            return operationUrl;
        }

        /// <summary>
        /// Gets the topics for a collection.
        /// </summary>
        /// <param name="operationUrl">The operation URL.</param>
        /// <returns></returns>
        public TopicResponse GetTopicResponse(string operationUrl)
        {
            return GetTopicResponseAsync(operationUrl).Result;
        }

        /// <summary>
        /// Gets the topics for a collection asynchronously.
        /// </summary>
        /// <param name="operationUrl">The operation URL.</param>
        /// <returns></returns>
        public async Task<TopicResponse> GetTopicResponseAsync(string operationUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(operationUrl);
            request.Headers["Ocp-Apim-Subscription-Key"] = this.ApiKey;
            request.Method = "GET";

            var responseData = "";

            using (var response = await request.GetResponseAsync())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseData = await reader.ReadToEndAsync();
                    }
                }
            }

            var topicResponse = JsonConvert.DeserializeObject<TopicResponse>(responseData);

            return topicResponse;
        }

        #endregion Methods
    }
}
