using colors_api.Database;
using colors_api.Models;

namespace colors_api.Services
{
    public class DbPaletteStorageService : IPaletteStorageService
    {
        private readonly AppDbContext _context;

        public DbPaletteStorageService(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public int AddPalette(ColorPaletteDto paletteDto)
        {
            var palette = new Palette
            {
                Name = $"Palette-{DateTime.UtcNow:yyyyMMdd-HHmmss}",
                Colors = paletteDto.Palette.Select(dto => new Color
                {
                    Type = dto.Type,
                    Red = dto.Red,
                    Green = dto.Green,
                    Blue = dto.Blue,
                    CreatedAt = DateTime.UtcNow
                }).ToArray()
            };

            _context.Palettes.Add(palette);
            _context.SaveChanges();

            return palette.Id;
        }

        public IReadOnlyList<ColorPaletteDto> GetAllPalettes()
        {
            return _context.Palettes
                .Select(p => new ColorPaletteDto(
                    p.Colors
                        .Select(c => new ColorDto(c.Type, c.Red, c.Green, c.Blue))
                        .ToArray()
                    ))
                .ToList();
        }

        public ColorPaletteDto? GetPaletteByIndex(int index)
        {
            var palette = _context.Palettes
                .Where(p => p.Id == index)
                .FirstOrDefault();

            if (palette == null)
            {
                return null;
            }

            return new ColorPaletteDto(
                palette.Colors
                    .Select(c => new ColorDto(c.Type, c.Red, c.Green, c.Blue))
                    .ToArray()
            );
        }
    }
}
