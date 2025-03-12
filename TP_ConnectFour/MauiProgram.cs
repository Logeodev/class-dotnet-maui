using Microsoft.Extensions.Logging;
using TP_ConnectFour.ViewModels;
using TP_ConnectFour.Views;
        
namespace TP_ConnectFour;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register services for dependency injection
        builder.Services.AddSingleton<ConnectFourGameViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<GameBoardView>();
        builder.Services.AddTransient<TokenView>();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
	}
}
