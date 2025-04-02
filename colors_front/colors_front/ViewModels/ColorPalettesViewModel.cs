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
        public ColorPalettesViewModel(IColorApiService api)
        {
            _colorApiService = api;
            this.IsBusy = false;
            RefreshCommand = new Command(async () => await LoadColorPalettesAsync());
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
