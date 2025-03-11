using ColorManager.ViewModels;

namespace ColorManager;

public partial class ColorsPage : ContentPage
{
    private ColorsViewModel _viewModel;
    
    public ColorsPage()
    {
        InitializeComponent();
        
        _viewModel = new ColorsViewModel();
        BindingContext = _viewModel;
    }

    async void OnColorSelected(object s, TappedEventArgs e)
    {
        if (e.Parameter is ColorItemViewModel selectedColor)
        {
            await Navigation.PushAsync(new ColorDetailPage(selectedColor, _viewModel));
        }
    }

    async void OnAddColorClicked(object sender, EventArgs e)
    {
        string colorName = await DisplayPromptAsync("Nouvelle couleur", "Nom de la couleur:", initialValue: "");

        if (!string.IsNullOrWhiteSpace(colorName))
        {
            Random random = new Random();
            Color randomColor = Color.FromRgb(
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));

            _viewModel.AddColor(new ColorItemViewModel(colorName, randomColor));
        }
    }
}