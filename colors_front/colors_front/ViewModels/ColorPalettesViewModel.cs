using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using colors_front.Models;
using colors_front.Services;

namespace colors_front.ViewModels
{
    public partial class ColorPalettesViewModel : ObservableObject
    {
        private readonly IColorApiService _colorApiService;

        [ObservableProperty]
        public bool _isBusy = false;

        public ObservableCollection<ColorPalette> ColorPalettes { get; } = new();
        public ICommand RefreshCommand { get; }
        public ICommand GeneratePaletteCommand { get; }

        public ColorPalettesViewModel(IColorApiService api)
        {
            _colorApiService = api;
            this.IsBusy = false;
            RefreshCommand = new Command(async () => await LoadColorPalettesAsync());
            GeneratePaletteCommand = new Command(async () => await GenerateNewPalette());
        }

        private async Task GenerateNewPalette()
        {
            IsBusy = true;

            await _colorApiService.AddGeneratedPaletteAsync();

            IsBusy = false;
        }

        public async Task LoadColorPalettesAsync()
        {
            IsBusy = true;

            var palettes = await _colorApiService.GetColorPalettesAsync();
            ColorPalettes.Clear();
            foreach (var palette in palettes)
            {
                ColorPalettes.Add(palette);
            }
            IsBusy = false;
        }
    }
}
