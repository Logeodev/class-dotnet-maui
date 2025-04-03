using colors_api.Models;

namespace colors_api.Services
{
    public interface IPaletteStorageService
    {
        IReadOnlyList<ColorPaletteDto> GetAllPalettes();
        ColorPaletteDto? GetPaletteByIndex(int index);
        int AddPalette(ColorPaletteDto palette);
    }
}
