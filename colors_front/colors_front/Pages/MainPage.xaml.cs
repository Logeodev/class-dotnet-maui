using colors_front.ViewModels;

namespace colors_front.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly ColorPalettesViewModel _viewModel;

        public MainPage(ColorPalettesViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.ColorPalettes.Count == 0)
            {
                await _viewModel.LoadColorPalettesAsync();
            }
        }
    }
}
