using TP_ConnectFour.ViewModels;

namespace TP_ConnectFour.Views;

public partial class TokenView : ContentView
{
    public TokenView()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        
        if (BindingContext is TokenViewModel viewModel)
        {
            // You could add additional setup here if needed
        }
    }
}
