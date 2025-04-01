namespace colors_api.Models
{
    public class ColorPaletteDto
    {
        public ColorDto[] Palette { get; set; }
        public ColorPaletteDto(ColorDto[] colors) {
            Palette = colors;
        }
    }
}
