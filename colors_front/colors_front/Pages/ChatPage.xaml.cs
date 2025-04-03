using System.Windows.Input;
using colors_front.ViewModels;

namespace colors_front.Pages;

public partial class ChatPage : ContentPage
{
    private readonly PromptViewModel _viewModel;
    public ChatPage(PromptViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
        BindingContext = _viewModel;
    }
}