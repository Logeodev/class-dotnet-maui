using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using colors_api.Models;
using colors_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace colors_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LlmController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LlmController> _logger;
        private readonly PaletteGeneratorService _paletteGeneratorService;
        private readonly IConfiguration _configuration;

        // URL du service Ollama dans votre docker-compose
        private const string OllamaServiceUrl = "http://colorsapi.ollama:11434";

        public LlmController(
            IHttpClientFactory httpClientFactory,
            ILogger<LlmController> logger,
            PaletteGeneratorService paletteGeneratorService,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _paletteGeneratorService = paletteGeneratorService;
            _configuration = configuration;
        }

        [HttpGet("generatePalette/{hint}")]
        public async Task<ActionResult<ColorPaletteDto>> GeneratePalette(string? hint = null)
        {
            var result = await _paletteGeneratorService.GeneratePaletteAsync(hint);

            if (result is null)
            {
                return BadRequest("Impossible de générer une palette");
            }

            return result;
        }

        [HttpPost("query")]
        public async Task<ActionResult<LlmQueryResponse>> Query(LlmQueryRequest request)
        {
            if (string.IsNullOrEmpty(request.Prompt))
            {
                return BadRequest("Le prompt ne peut pas être vide");
            }

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromMinutes(1);

                var ollamaRequest = new OllamaRequest
                {
                    Model = request.Model ?? "llama3.2:1b",
                    Prompt = request.Prompt
                };

                var response = await client.PostAsJsonAsync($"{OllamaServiceUrl}/api/generate", ollamaRequest);
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Erreur lors de la communication avec Ollama: {StatusCode}", response.StatusCode);
                    return StatusCode((int)response.StatusCode, "Erreur lors de la communication avec le modèle LLM");
                }

                var ollamaResponse = await response.Content.ReadFromJsonAsync<OllamaResponse>();
                
                stopwatch.Stop();

                return new LlmQueryResponse
                {
                    Response = ollamaResponse?.Response ?? "Pas de réponse",
                    Model = ollamaResponse?.Model ?? ollamaRequest.Model,
                    ProcessingTime = stopwatch.Elapsed
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors du traitement de la requête LLM");
                return StatusCode(500, "Une erreur s'est produite lors du traitement de la requête");
            }
        }
    }
}
