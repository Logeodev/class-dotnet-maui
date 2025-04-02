using colors_front.Models;

namespace colors_front.Services
{
    public interface IColorApiService
    {
        Task<List<ColorPalette>> GetColorPalettesAsync();
        Task<ColorPalette?> GetColorPaletteByIndexAsync(int index);
    }
}
