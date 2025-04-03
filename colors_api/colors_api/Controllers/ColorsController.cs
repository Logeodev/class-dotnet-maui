using colors_api.Models;
using colors_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace colors_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColorsController : ControllerBase
    {
        private readonly IPaletteStorageService _paletteStorageService;
        private readonly PaletteGeneratorService _paletteGeneratorService;
        private readonly ILogger<ColorsController> _logger;

        public ColorsController(
            IPaletteStorageService paletteStorageService,
            PaletteGeneratorService paletteGeneratorService,
            ILogger<ColorsController> logger)
        {
            _paletteStorageService = paletteStorageService;
            _paletteGeneratorService = paletteGeneratorService;
            _logger = logger;
        }

        [HttpGet(Name = "GetPalettes")]
        public IActionResult Get()
        {
            return Ok(new { Items = _paletteStorageService.GetAllPalettes() });
        }

        [HttpGet("{index}", Name = "GetPaletteAtIndex")]
        public IActionResult Get(int index)
        {
            var found = _paletteStorageService.GetPaletteByIndex(index);
            if (found is null)
            {
                return NotFound($"Palette avec l'index {index} non trouvée");
            }
            return Ok(new { Item = found });
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateAndAddPalette([FromQuery] string? hint = null)
        {
            try
            {
                var generatedPalette = await _paletteGeneratorService.GeneratePaletteAsync(hint);

                if (generatedPalette is null)
                {
                    return BadRequest("Impossible de générer une nouvelle palette");
                }

                int newIndex = _paletteStorageService.AddPalette(generatedPalette);

                return CreatedAtRoute("GetPaletteAtIndex", new { index = newIndex },
                    new { Item = generatedPalette, Index = newIndex });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération et de l'ajout de la palette");
                return StatusCode(500, "Une erreur s'est produite lors de la génération de la palette");
            }
        }
    }
}
