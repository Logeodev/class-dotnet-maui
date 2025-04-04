using System.Net.Http.Json;
using System.Text.Json;
using colors_front.Models;

namespace colors_front.Services
{
    public class ColorsApiService : IColorApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly string _baseUrl;

        public ColorsApiService()
        {
            _httpClient = new HttpClient();
            _jsonSerializerOptions = new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true
            };
            _baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        }
        public async Task<ColorPalette?> GetColorPaletteByIndexAsync(int index)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/colors/{index}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ItemApiResponse<ColorPalette>>(content, _jsonSerializerOptions);

                return apiResponse?.Item;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving color palette at index {index}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ColorPalette>> GetColorPalettesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/colors");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ItemsApiResponse<ColorPalette>>(content, _jsonSerializerOptions);

                return apiResponse?.Items?.ToList() ?? new List<ColorPalette>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving color palettes: {ex.Message}");
                return new List<ColorPalette>();
            }
        }

        public async Task<ColorPalette?> AddGeneratedPaletteAsync(string? hint = null)
        {
            try
            {
                AddPaletteApi addPaletteApi = new AddPaletteApi
                {
                    Hint = hint,
                    Palette = null
                };
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/colors/generate",  addPaletteApi);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var newPalette = JsonSerializer.Deserialize<ItemApiResponse<ColorPalette>>(content, _jsonSerializerOptions);
                return newPalette?.Item;
            } catch (Exception ex)
            {
                Console.WriteLine($"Error creating new palete with LLM : {ex.Message}");
                return null;
            }
        }

        public Task<bool> DeletePaletteByIndexAsync(ColorPalette palette)
        {
            try
            {
                var response = _httpClient.PostAsJsonAsync(
                    $"{_baseUrl}/colors/deletePalette", 
                   JsonSerializer.Serialize(palette, _jsonSerializerOptions)
                );
            } catch (Exception ex)
            {
                Console.WriteLine($"Error deleting palette: {ex.Message}");
                return Task.Run(() => false);
            }
            return Task.Run(() => true);
        }
    }
}
