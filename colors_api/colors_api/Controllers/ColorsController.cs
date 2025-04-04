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

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateAndAddPalette(AddPaletteDto request)
        {
            try
            {
                ColorPaletteDto? generatedPalette = null;
                if (request.Palette is null)
                {
                    generatedPalette = await _paletteGeneratorService.GeneratePaletteAsync(request.Hint);
                }
                else if (request.Palette is not null)
                {
                    generatedPalette = request.Palette;
                }

                if (generatedPalette is null)
                {
                    return BadRequest("Impossible de générer une nouvelle palette");
                }

                int newIndex = _paletteStorageService.AddPalette(generatedPalette);

                return CreatedAtRoute("GetPaletteAtIndex", new { index = newIndex },
                    new { Item = generatedPalette });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la génération et de l'ajout de la palette");
                return StatusCode(500, "Une erreur s'est produite lors de la génération de la palette");
            }
        }

        [HttpPost("deletePalette")]
        public async Task<IActionResult> DeletePalette(ColorPaletteDto palette)
        {
            var palettes = _paletteStorageService.GetAllPalettes().ToList();
            int foundPaletteId = palettes.FindIndex(
                p => p.Palette.All(c => palette.Palette.Contains(c))
            );
            if (foundPaletteId != -1)
            {
                _paletteStorageService.RemovePalette(foundPaletteId);
                return Ok();
            }
            return NotFound("Palette non trouvée pour la suppression");
        }
    }
}
