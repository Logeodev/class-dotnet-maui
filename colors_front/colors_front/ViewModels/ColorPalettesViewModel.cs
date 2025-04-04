using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using colors_front.Models;
using colors_front.Services;
using System.Runtime.CompilerServices;

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
        public ICommand DeletePaletteCommand { get; }

        public ColorPalettesViewModel(IColorApiService api)
        {
            _colorApiService = api;
            this.IsBusy = false;
            RefreshCommand = new Command(async () => await LoadColorPalettesAsync());
            GeneratePaletteCommand = new Command(async () => await GenerateNewPalette());
            DeletePaletteCommand = new Command<int>(async (id) => await DeletePalette(id));
        }

        private async Task DeletePalette(int id)
        {
            var paletteToDelete = ColorPalettes.ElementAt(id);
            if (paletteToDelete != null)
            {
                ColorPalettes.Remove(paletteToDelete);
                await _colorApiService.DeletePaletteByIndexAsync(paletteToDelete);
            }
        }

        private async Task GenerateNewPalette()
        {
            IsBusy = true;

            var addedPalette = await _colorApiService.AddGeneratedPaletteAsync();
            if (addedPalette != null)
            {
                ColorPalettes.Insert(0, addedPalette);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to generate a new color palette.", "OK");
            }

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
