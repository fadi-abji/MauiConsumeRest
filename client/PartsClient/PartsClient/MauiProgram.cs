using PartsClient.Pages;
using PartsClient.Service;
using PartsClient.ViewModels;

namespace PartsClient;

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

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7210") });

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IPartsService, PartsService>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<PartsPage>();
        builder.Services.AddSingleton<AddPartPage>();
        builder.Services.AddSingleton<AddPartViewModel>();
        builder.Services.AddSingleton<PartsViewModel>();


        return builder.Build();
    }
}
