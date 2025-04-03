using System.Text.Json.Serialization;

namespace colors_front.Models
{
    public class PromptApiRequest
    {
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = "";
    }

    public class PromptApiResponse
    {
        [JsonPropertyName("response")]
        public string Response { get; set; } = "";
        [JsonPropertyName("model")]
        public string Model { get; set; } = "";
        [JsonPropertyName("processingTime")]
        public TimeSpan ProcessingTime { get; set; }
    }
}
