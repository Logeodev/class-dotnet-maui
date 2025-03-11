using ColorManager.ViewModels;

namespace ColorManager;

public partial class ColorDetailPage : ContentPage
{
    private ColorItemViewModel _viewModel;
    private ColorsViewModel _colorsViewModel;

    public ColorDetailPage(ColorItemViewModel colorItemViewModel, ColorsViewModel colorsViewModel)
    {
        InitializeComponent();
        _viewModel = colorItemViewModel;
        _colorsViewModel = colorsViewModel;
        BindingContext = _viewModel;
    }

    async void OnDeleteColorClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmation", "Voulez-vous vraiment supprimer cette couleur ?", "Oui", "Non");
        if (confirm)
        {
            _colorsViewModel.RemoveColor(_viewModel);
            await Navigation.PopAsync();
        }
    }

    async void OnChangeName(object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        _viewModel.Name = e.NewTextValue;
    }
}
