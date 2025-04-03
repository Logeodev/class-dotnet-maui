using colors_api.Models;
using System.Net.Http.Json;

namespace colors_api.Services
{
    public class PaletteGeneratorService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PaletteGeneratorService> _logger;

        // URL du service Ollama dans votre docker-compose
        private const string OllamaServiceUrl = "http://colorsapi.ollama:11434";

        public PaletteGeneratorService(IHttpClientFactory httpClientFactory, ILogger<PaletteGeneratorService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<ColorPaletteDto?> GeneratePaletteAsync(string? hint = null)
        {
            var ollamaRequest = new OllamaRequest
            {
                Model = "llama3.2:1b",
                Prompt = "Generate 5 RGB colors to make an UI palette. These colors should be harmonious in app UI considerations"
                    + (hint is not null ? " and with " + hint + ". " : ". ") +
                    "Answer with only the JSON as {\"palette\": " +
                    "[{\"type\": <int between 0 to 4>, \"red\": <int 0 to 255>, \"green\": <int 0 to 255>, \"blue\": <int 0 to 255>}]} " +
                    "Give me this JSON with 5 colors."
            };

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromMinutes(2);

                var response = await client.PostAsJsonAsync($"{OllamaServiceUrl}/api/generate", ollamaRequest);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Erreur lors de la communication avec Ollama: {StatusCode}, {Content}", response.StatusCode, response.Content);
                    return null;
                }

                // D'abord, récupérer la réponse brute
                var ollamaResponse = await response.Content.ReadFromJsonAsync<OllamaResponse>();

                if (ollamaResponse == null)
                {
                    _logger.LogError("La réponse d'Ollama est nulle");
                    return null;
                }

                try
                {
                    string jsonContent = ollamaResponse.Response;

                    int startIndex = jsonContent.IndexOf('{');
                    int endIndex = jsonContent.LastIndexOf('}');

                    if (startIndex >= 0 && endIndex > startIndex)
                    {
                        jsonContent = jsonContent.Substring(startIndex, endIndex - startIndex + 1);
                        // Désérialiser la chaîne JSON en ColorPaletteDto
                        var colorPalette = System.Text.Json.JsonSerializer.Deserialize<ColorPaletteDto>(
                            jsonContent,
                            new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        return colorPalette;
                    }
                    else
                    {
                        _logger.LogError("Format JSON invalide dans la réponse: {Response}", ollamaResponse.Response);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erreur lors de l'analyse de la réponse JSON: {Response}", ollamaResponse.Response);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération de la palette: {Message}", ex.Message);
                return null;
            }
        }
    }
}
