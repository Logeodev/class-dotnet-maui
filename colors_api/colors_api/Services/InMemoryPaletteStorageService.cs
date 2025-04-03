using colors_api.Models;

namespace colors_api.Services
{
    public class InMemoryPaletteStorageService : IPaletteStorageService
    {
        private readonly List<ColorPaletteDto> _palettes = new()
        {
            new ColorPaletteDto([
                new ColorDto(ColorType.Primary, 255,0,0),
                new ColorDto(ColorType.Secondary, 0, 255, 0),
                new ColorDto(ColorType.Tertiary, 0, 0, 255),
                new ColorDto(ColorType.Complementary, 255, 255, 0),
                new ColorDto(ColorType.Accent, 0, 255, 255)
            ]),
            new ColorPaletteDto([
                new ColorDto(ColorType.Primary, 255,15,0),
                new ColorDto(ColorType.Secondary, 185, 56, 75),
                new ColorDto(ColorType.Tertiary, 64, 0, 111),
                new ColorDto(ColorType.Complementary, 87, 94, 23),
                new ColorDto(ColorType.Accent, 48, 21, 113)
            ])
        };

        public IReadOnlyList<ColorPaletteDto> GetAllPalettes() => _palettes.AsReadOnly();

        public ColorPaletteDto? GetPaletteByIndex(int index)
        {
            if (index >= 0 && index < _palettes.Count)
            {
                return _palettes[index];
            }
            return null;
        }

        public int AddPalette(ColorPaletteDto palette)
        {
            _palettes.Add(palette);
            return _palettes.Count - 1;
        }
    }
}
