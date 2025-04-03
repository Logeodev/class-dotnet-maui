using colors_front.Services;
using colors_front.ViewModels;
using Microsoft.Extensions.Logging;

namespace colors_front
{
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

            builder.Services.AddSingleton<IColorApiService, ColorsApiService>();
            builder.Services.AddSingleton<ColorPalettesViewModel>();

            builder.Services.AddSingleton<LamaApiService>();
            builder.Services.AddSingleton<PromptViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
