using System.Net.Http.Headers;
using System.Text.Json;
using colors_front.Models;

namespace colors_front.Services
{
    public class LamaApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly string _baseUrl;

        public LamaApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(2);
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000/api/llm" : "http://localhost:5000/api/llm";
        }
        public async Task<PromptApiResponse?> GetPromptAnswer(string prompt)
        {
            try
            {
                var data = JsonSerializer.Serialize(new PromptApiRequest{ Prompt=prompt });
                var response = await _httpClient.PostAsync($"{_baseUrl}/query", new StringContent(data, MediaTypeHeaderValue.Parse("application/json")));
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<PromptApiResponse>(content, _jsonSerializerOptions);

                return apiResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error communicationg with LLM : {ex.Message}");
                return null;
            }
        }
    }
}
