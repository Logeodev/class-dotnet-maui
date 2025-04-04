namespace colors_api.Models
{
    public class AddPaletteDto
    {
        public string? Hint { get; set; }

        public ColorPaletteDto? Palette { get; set; }
    }
}
