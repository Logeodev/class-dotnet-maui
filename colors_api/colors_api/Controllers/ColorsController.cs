using colors_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace colors_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColorsController : ControllerBase
    {
        private static readonly ColorPaletteDto[] ColorPalettes = [ 
            new ColorPaletteDto([
                    new ColorDto(ColorType.Primary, 255,0,0), 
                    new ColorDto(ColorType.Secondary, 0, 255, 0), 
                    new ColorDto(ColorType.Tertiary, 0, 0, 255),
                    new ColorDto(ColorType.Complementary, 255, 255, 0),
                    new ColorDto(ColorType.Accent, 0, 255, 255)
                ]),
            new ColorPaletteDto([
                    new ColorDto(ColorType.Primary, 255,0,0),
                    new ColorDto(ColorType.Secondary, 0, 255, 0),
                    new ColorDto(ColorType.Tertiary, 0, 0, 255),
                    new ColorDto(ColorType.Complementary, 255, 255, 0),
                    new ColorDto(ColorType.Accent, 0, 255, 255)
                ]),
        ];

        [HttpGet(Name = "GetPalettes")]
        public IActionResult Get()
        {
            return Ok(new { Items = ColorPalettes });
        }

        [HttpGet("{index}", Name = "GetPaletteAtIndex")]
        public IActionResult Get(int index)
        {
            if (index < 0 || index >= ColorPalettes.Length)
            {
                return NotFound();
            }
            var found = ColorPalettes.GetValue(index);
            return Ok(new {Item = found});
        }
    }
}
