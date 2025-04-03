using System.Windows.Input;
using colors_front.Models;
using colors_front.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace colors_front.ViewModels
{
    public partial class PromptViewModel : ObservableObject
    {
        [ObservableProperty]
        public string _prompt = string.Empty;
        [ObservableProperty]
        public string _response = string.Empty;
        [ObservableProperty]
        public TimeSpan _responseTime = TimeSpan.Zero;
        [ObservableProperty]
        public bool _isVisible = false;

        public ICommand SendCommand { get; }
        private LamaApiService _lamaApi;

        public PromptViewModel(LamaApiService lamaApi)
        {
            _lamaApi = lamaApi;
            SendCommand = new Command(async () => await SendPrompt());
        }

        public async Task SendPrompt()
        {
            var res = await _lamaApi.GetPromptAnswer(Prompt);
            if (res is null)
            {
                return;
            }
            Response = res.Response;
            ResponseTime = res.ProcessingTime;
        }
    }
}
