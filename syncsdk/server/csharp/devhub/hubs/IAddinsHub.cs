using Newtonsoft.Json;

namespace Microsoft.Skype.Interviews.Samples.DevHub.hubs
{
    public interface IAddinsHub
    {
        /// <summary>
        /// Sends the addin message.
        /// </summary>
        /// <param name="message">The message.</param>
        void messageReceived(AddinMessageRequest message);

        /// <summary>
        /// Sends to clients addin session content
        /// </summary>
        /// <param name="content">The content.</param>
        void contextFetched(string content);
    }

    public class AddinMessageRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}