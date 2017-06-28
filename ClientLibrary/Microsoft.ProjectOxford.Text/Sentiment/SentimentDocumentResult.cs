using Newtonsoft.Json;

namespace Microsoft.ProjectOxford.Text.Sentiment
{
    /// <summary>
    /// Sentiment detected by the Text Analytics API.
    /// </summary>
    public class SentimentDocumentResult
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier of the document.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the score (0 = negative; 1 = positive).
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        [JsonProperty("score")]
        public float Score { get; set; }

        #endregion Properties
    }
}
