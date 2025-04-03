using System.Text.Json.Serialization;

namespace colors_api.Models
{
    public class ColorPaletteDto
    {
        [JsonPropertyName("palette")]
        public ColorDto[] Palette { get; set; }
        public ColorPaletteDto(ColorDto[] colors) {
            Palette = colors;
        }
        public ColorPaletteDto()
        {
            
        }
    }
}
