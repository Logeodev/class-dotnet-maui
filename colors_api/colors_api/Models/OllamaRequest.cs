using System.Text.Json.Serialization;

namespace colors_api.Models
{ 
    public class OllamaRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "llama3";

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = "";

        [JsonPropertyName("stream")]
        public bool Stream { get; set; } = false;

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; } = 0.7f;

        [JsonPropertyName("max_tokens")]
        public int? MaxTokens { get; set; } = null;
    }

    public class OllamaResponse
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "";

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("response")]
        public string Response { get; set; } = "";

        [JsonPropertyName("done")]
        public bool Done { get; set; }
    }

    public class LlmQueryRequest
    {
        public string Prompt { get; set; } = "";
        public string? Model { get; set; }
    }

    public class LlmQueryResponse
    {
        public string Response { get; set; } = "";
        public string Model { get; set; } = "";
        public TimeSpan ProcessingTime { get; set; }
    }
}
